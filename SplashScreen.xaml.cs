using System.Windows;

public partial class SplashScreen
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartUp(e);

        var splashScreen = new SplashScreenWindow();
        this.MainWindow = splashScreen;
        splashScreen.Show();

        Task.Factory.StartNew(() =>
        {
            System.Threading.Thread.Sleep(3000);

            this.Dispatcher.Invoke(() =>
            {
                var mainWindow = new MainWindow();
                this.MainWindow = mainWindow;
                mainWindow.Show();
                splashScreen.Close();
            });
        });
    }
}