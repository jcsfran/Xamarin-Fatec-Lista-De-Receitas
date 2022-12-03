using JulioCesarApp.Helper;
using JulioCesarApp.View.Receita;
using Xamarin.Forms;
using System.IO;
using System;

namespace JulioCesarApp
{
    public partial class App : Application
    {
        static SqLiteDatabaseHelper _db;

        public static SqLiteDatabaseHelper Database
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                                Environment.SpecialFolder.LocalApplicationData
                            ),
                        "banco_sqllite_compras.db3"
                        );

                    _db = new SqLiteDatabaseHelper(path);
                }

                return _db;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Listagem());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
