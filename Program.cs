namespace ORM
{
    using System;
    using System.Windows.Forms;

    public static class Program
    {
        /// <summary>
        /// ������� ����� ����� ��� ����������.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            EFlogger.EntityFramework6.EFloggerFor6.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
