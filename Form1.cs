namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    /// <inheritdoc />
    public partial class Form1 : Form
    {
        private readonly List<int> owners = new List<int>();

        private readonly List<string> types = new List<string>();

        public Form1()
        {
            InitializeComponent();
            DgvPartsIntialize();
            DgvInfoIntialize();
        }

        #region Инициализация dgv

        private void DgvPartsIntialize()
        {
            // Добавление в ComboBox списка пациентов
            using (var readContext = new MorgueEntities())
            {
                foreach (var owner in readContext.Patients)
                {
                    owners.Add(owner.id);
                }
            }

            #region Содание столбцов dgv

            dgvParts.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "ownerID",
                HeaderText = @"ID владельца",
                DataSource = owners,
                ValueType = typeof(int)
            });

            dgvParts.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "type",
                HeaderText = @"Тип",
                DataSource = types,
            });

            dgvParts.Columns.Add(new CalendarColumn
            {
                Name = "date",
                HeaderText = @"Дата изъятия"
            });

            #endregion

            // Заполнение dgv
            DgvPartsUpdate();
            dgvParts.AutoResizeColumns();
        }

        private void DgvInfoIntialize()
        {
            #region Создание столбцов dgv

            dgvInfo.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "type",
                HeaderText = @"Тип",
                MaxInputLength = 50
            });

            dgvInfo.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "shelfLife",
                HeaderText = @"Максимальный срок хранения"
            });

            dgvInfo.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "fragility",
                HeaderText = @"Хрупкое?"
            });

            #endregion

            // Заполнение dgv данными
            DgvInfoUpdate();
            dgvParts.AutoResizeColumns();
        }

        #endregion

        #region Обновление содержимого dgv

        private void DgvPartsUpdate()
        {
            // Очищение списка типов частей тела
            types.Clear();

            using (var readContext = new MorgueEntities())
            {
                // Обновление списка типов частей тела
                foreach (var part in readContext.BodyPartsInfoes)
                {
                    types.Add(part.type);
                }

                #region Заполнение dgvParts

                foreach (var part in readContext.BodyParts)
                {
                    var row = dgvParts.Rows.Add(part.ownerID, part.type, part.date);
                    dgvParts.Rows[row].Tag = part;
                    var cells = dgvParts.Rows[row].Cells;
                    cells[0].ReadOnly = true;
                    cells[1].ReadOnly = true;
                }

                #endregion
            }
        }

        private void DgvInfoUpdate()
        {
            // Очищение полей dgvInfo
            dgvInfo.Rows.Clear();

            #region Заполнение dgvInfo

            using (var readContext = new MorgueEntities())
            {
                foreach (var partsInfo in readContext.BodyPartsInfoes)
                {
                    var row = dgvInfo.Rows.Add(partsInfo.type, partsInfo.shelfLife, partsInfo.fragility);
                    dgvInfo.Rows[row].Tag = partsInfo;
                    dgvInfo.Rows[row].Cells["type"].ReadOnly = true;
                }
            }

            #endregion
        }

        #endregion

        #region Обработка ошибок данных

        private void DgvPartsDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void DgvInfoDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        #endregion

        #region Изменение значения ячеек

        private void DgvPartsCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgvParts.Rows[e.RowIndex].IsNewRow)
            {
                dgvParts[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и не сохранено.";
            }
        }

        private void DgvInfoCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgvInfo.Rows[e.RowIndex].IsNewRow)
            {
                dgvInfo[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и не сохранено.";
            }
        }

        #endregion

        #region Удаление объектов

        private void DgvPartsUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
           using (var deleteContext = new MorgueEntities())
           {
               // Удаление несохраненного в БД объекта из dgv
               if (e.Row.Tag == null)
               {
                   return;
               }

               // Добавление удаляемого объекта к контексту
               deleteContext.BodyParts.Attach((BodyPart)e.Row.Tag);

               // Удаление найденного объекта
               deleteContext.BodyParts.Remove((BodyPart)e.Row.Tag);

               // Сохранение изменений
               deleteContext.SaveChanges();
           }
        }

        private void DgvInfoUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var deleteContext = new MorgueEntities())
            {
                // Удаление несохраненного в БД объекта из dgv
                if (e.Row.Cells[0].Value == null || e.Row.Cells[1].Value == null || e.Row.Tag == null)
                {
                    return;
                }

                // Добавление удаляемого объекта к контексту
                deleteContext.BodyPartsInfoes.Attach((BodyPartsInfo)e.Row.Tag);

                // Проверка наличия частей тела данного типа
                if (((BodyPartsInfo)e.Row.Tag).BodyParts.Count != 0)
                {
                    e.Row.ErrorText = @"Вы не можете удалить информацию о части тела, не удалив все ее экземпляры";
                    e.Cancel = true;
                    return;
                }

                // Удаление найденного объекта
                deleteContext.BodyPartsInfoes.Remove((BodyPartsInfo)e.Row.Tag);
                types.Remove(((BodyPartsInfo)e.Row.Tag).type);

                // Сохранение изменений
                deleteContext.SaveChanges();
            }
        }

        #endregion

        #region Вставка/изменение объектов с проверкой содержимого

        private void DgvPartsRowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!dgvParts.IsCurrentRowDirty)
            {
                return;
            }

            var row = dgvParts.Rows[e.RowIndex];

            var newOwnerId = (int?)row.Cells["ownerId"].Value;
            var newType = (string)row.Cells["type"].Value;
            var newDate = (DateTime)row.Cells["date"].Value;
            if (newDate > DateTime.Now)
            {
                row.Cells["date"].ErrorText = @"Дата не может быть позже сегодняшнего дня";
                return;
            }

            using (var readWriteContext = new MorgueEntities())
            {
                #region Поиск объектов, связанных с добавляемым/изменяемым с помощью навигационных свойств

                Patient newPatient;
                try
                {
                    newPatient = readWriteContext.Patients.Single(t => t.id == newOwnerId);
                }
                catch (InvalidOperationException)
                {
                    dgvParts.Rows[e.RowIndex].Cells["ownerId"].ErrorText = @"Некорректное значение";
                    return;
                }

                BodyPartsInfo newInfo;
                try
                {
                    newInfo = readWriteContext.BodyPartsInfoes.Single(t => t.type == newType);
                }
                catch (InvalidOperationException)
                {
                    dgvParts.Rows[e.RowIndex].Cells["type"].ErrorText = @"Некорректное значение";
                    return;
                }

                #endregion

                // Проверка наличия объекта в бд
                if (row.Tag == null)
                {
                    #region Проверка уникальности ключа объекта

                    if (readWriteContext.BodyParts.FirstOrDefault(t =>
                            t.ownerID == newOwnerId && t.type == newType) != null)
                    {
                        row.Cells[0].ErrorText = "Нельзя добавить объект c неуникальным первичным ключом";
                        row.Cells[1].ErrorText = "Нельзя добавить объект c неуникальным первичным ключом";
                        return;
                    }

                    #endregion

                    #region Добавление объекта

                    // Создание нового объекта
                    var newPart = new BodyPart
                    {
                        ownerID = (int)newOwnerId,
                        type = newType,
                        date = newDate,
                        Patient = newPatient,
                        BodyPartsInfo = newInfo
                    };

                    // Добавление нового объекта в БД
                    readWriteContext.BodyParts.Add(newPart);

                    // Обновление тэга
                    row.Tag = newPart;
                    row.Cells[0].ReadOnly = true;
                    row.Cells[1].ReadOnly = true;

                    #endregion
                }
                else
                {
                    #region Обновление полей существующего объекта
                    // Поиск обновляемого объекта в БД 
                    var updatedPart =
                        readWriteContext.BodyParts.Single(t => t.ownerID == newOwnerId && t.type == newType);

                    // Обновление поля
                    updatedPart.date = newDate;

                    // Обновление строки в dgv
                    dgvParts.Rows[e.RowIndex].SetValues(newOwnerId, newType, newDate);

                    #endregion
                }

                // Сохранение данных
                readWriteContext.SaveChanges();
            }

            // Удаление ошибок
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.ErrorText = string.Empty;
            }

            // TODO: Добавить проверку перед добавлением и проверку на дубликаты в обеих dgv
        }

        private void DgvInfoRowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!dgvInfo.IsCurrentRowDirty)
            {
                return;
            }

            var row = dgvInfo.Rows[e.RowIndex];

            // Проверка поля "type"
            var newType = (string)row.Cells["type"].Value;
            if (newType == null)
            {
                row.Cells["type"].ErrorText = "Это поле не может быть пустым";
                return;
            }

            // Проверка поля "shelfLife"
            int newShelfLife;
            try
            {
                newShelfLife = int.Parse((string)row.Cells["shelfLife"].Value);
            }
            catch (ArgumentNullException)
            {
                row.Cells["shelfLife"].ErrorText = "Это поле не может быть пустым";
                return;
            }
            catch (OverflowException)
            {
                row.Cells["shelfLife"].ErrorText = "Это число недопустимо";
                return;
            }
            catch (InvalidCastException)
            {
                newShelfLife = (int)row.Cells["shelfLife"].Value;
            }
            catch (FormatException)
            {
                row.Cells["shelfLife"].ErrorText = "Срок хранения должен быть целым числом";
                return;
            }

            if (newShelfLife < 0)
            {
                row.Cells["shelfLife"].ErrorText = "Срок хранения не может быть отрицательным";
                return;
            }

            // Проверка поля "fragility"
            bool newFragility;
            try
            {
                newFragility = (bool)row.Cells["fragility"].Value;
            }
            catch (Exception)
            {
                newFragility = false;
            }

            using (var readWriteContext = new MorgueEntities())
            {
                // Проверка наличия объекта в БД
                if (row.Tag == null)
                {
                    #region Добавление объекта

                    if (readWriteContext.BodyPartsInfoes.Count(t => t.type == newType) != 0)
                    {
                        dgvInfo.Rows[e.RowIndex].Cells[0].ErrorText = "Такой объект уже существует";
                        return;
                    }

                    // Создание объекта
                    var newPartInfo = new BodyPartsInfo
                    {
                        type = newType,
                        shelfLife = newShelfLife,
                        fragility = newFragility
                    };

                    // Добавление объекта в БД
                    readWriteContext.BodyPartsInfoes.Add(newPartInfo);

                    // Обновление тэга
                    row.Tag = newPartInfo;
                    row.Cells[0].ReadOnly = true;
                    types.Add(newType);

                    #endregion
                }
                else
                {
                    #region Обновление полей существующего объекта

                    // Поиск обновляемого объекта в БД 
                    var updatedPartInfo = readWriteContext.BodyPartsInfoes.Single(t => t.type == newType);

                    // Обновление полей
                    updatedPartInfo.shelfLife = newShelfLife;
                    updatedPartInfo.fragility = newFragility;

                    row.Tag = updatedPartInfo;

                    // Обновление строки в dgv
                    row.SetValues(newType, newShelfLife, newFragility);

                    #endregion   
                }

                // Сохранение изменений
                readWriteContext.SaveChanges();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.ErrorText = string.Empty;
                }
            }
        }

        #endregion
    }
}