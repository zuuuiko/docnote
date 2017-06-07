using System.Windows;
using GalaSoft.MvvmLight.Threading;
using System.Windows.Markup;
using System.Globalization;
using System.Threading;
using System.Data.Entity;

namespace docnote
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("uk-UA");

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
    new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            using (var context = new Model.DocnoteContext())
            {
#if DEBUG
                Database.SetInitializer(new Model.ModelInitializer());
#else
                if (!context.Database.Exists())
                {
                    Database.SetInitializer(new CreateDatabaseIfNotExists<Model.DocnoteContext>());
                }
                else if (!context.Database.CompatibleWithModel(false))
                {
                    Database.SetInitializer(new MigrateDatabaseToLatestVersion<Model.DocnoteContext, Migrations.Configuration>("ConnectionStringDocnoteDB"));
                }
                else
                {
                    Database.SetInitializer<Model.DocnoteContext>(null);
                }
#endif
                context.Database.Initialize(false);
            }

        }
    }
}
