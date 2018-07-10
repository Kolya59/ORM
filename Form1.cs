using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;
using ORM.Models;

namespace ORM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvPartsIntialize();
            dgvInfoIntialize();
        }

        private void dgvPartsIntialize()
        {
            dgvParts.Columns.Add("ownerID","ID владельца");
            dgvParts.Columns.Add("type","Тип");
            dgvParts.Columns.Add(new CalendarColumn
            {
                Name = "date",
                HeaderText = @"Дата изъятия"
            });
            dgvPartsUpdate();
            dgvParts.AutoResizeColumns();
        }

        private void dgvInfoIntialize()
        {
            dgvInfo.Columns.Add("type","Тип");
            dgvInfo.Columns.Add("shelfLife","Максимальный срок хранения");
            dgvInfo.Columns.Add("fragility","Хрупкое?");
            dgvInfoUpdate();
            dgvParts.AutoResizeColumns();
        }

        private void dgvPartsUpdate()
        {
            using (var db = new MorgueContext())
            {
                foreach (var part in db.BodyParts)
                {
                    dgvParts.Rows.Add(part.ownerID, part.type, part.date);
                }
            }
        }

        private void dgvInfoUpdate()
        {
            using (var db = new MorgueContext())
            {
                foreach (var partsInfo in db.BodyPartsInfoes)
                {
                    dgvInfo.Rows.Add(partsInfo.type, partsInfo.shelfLife, partsInfo.fragility);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
