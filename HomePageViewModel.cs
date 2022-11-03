using MAGIApp.Constants;
using MAGIApp.Helpers;
using MAGIApp.Interface;
using MAGIApp.Model.RequestModel;
using MAGIApp.Model.ResponseModel;
using MAGIApp.Services;
using MAGIApp.Views;
using MAGIApp.Utils;
using MAGIApp.Views.DashboardScreens;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.AspNet.SignalR.Client;
using System.Text.RegularExpressions;
using MAGIApp.Model;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Linq;
using System.Timers;

namespace MAGIApp.ViewModel
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public static HubConnection hubConnection;
        public static IHubProxy chatHubProxy;
        public Command ConnectCommand { get; }
        public Command DisconnectCommand { get; }
        private bool _IsDevelopment;
        public bool IsDevelopment
        {
            get { return _IsDevelopment; }
            set
            {
                _IsDevelopment = value;
                OnPropertyChanged(nameof(_IsDevelopment));
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        async Task Connect()
        {
            try
            {
                await hubConnection.Start();
                string connId = hubConnection.ConnectionId;
                if (!string.IsNullOrEmpty(connId))
                {
                    Name = Util.DisplayName;
                    string MyID = Util.Userid;
                    var deviceId = Preferences.Get("my_deviceId", string.Empty);
                    if (string.IsNullOrEmpty(deviceId))
                    {
                        deviceId = Guid.NewGuid().ToString();
                        Preferences.Set("my_deviceId", deviceId);
                    }
                    await chatHubProxy.Invoke("Onconnected", new object[] { MyID, deviceId, Name, "Mobile-Page", false });
                    Helper.ShowToast("Chat Connected");
                }
            }
            catch (Exception ex)
            {
                string message = $"Connection error: {ex.Message}";
                ConnectCommand.Execute(null);
            }
        }

        public ICommand ButtonCommand { get; private set; }


        private Color _item1BorderColor = Color.Transparent;
        private Color _item1BacgroundColor = Color.FromHex("#FEEECA");
        private bool _isGridVisible = true;
        private bool _isListVisible = false;
        private Color frameL1Bgcolor = Color.FromHex("#FEEECA");
        public ICommand Item1Command { get; private set; }
        public ICommand OpenMenuDrawer { get; private set; }
        public ICommand ListCommnad { get; private set; }
        public ICommand FrameL1Command { get; private set; }
        public ICommand ProgramL1Command { get; private set; }
        public ICommand ProgramCommand { get; private set; }
        public ICommand MyScheduleCommand { get; private set; }
        public ICommand SpeakerCommand { get; private set; }
        public ICommand OrganizationCommand { get; private set; }
        public ICommand EventsCommand { get; private set; }
        public ICommand MyMeetingsCommand { get; private set; }
        public ICommand EventContactCommand { get; private set; }
        public ICommand NetworkCommand { get; private set; }
        public ICommand TwitterCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand FindPeopleCommand { get; private set; }
        public ICommand MyFeedbackCommand { get; private set; }
        public ICommand CalendarCommand { get; private set; }
        public ICommand AttendeeCommand { get; private set; }
        public ICommand SpeakerSurveyCommand { get; private set; }
        public ICommand ExhibitorSurveyCommand { get; private set; }
        public ICommand NonAttendeeCommand { get; private set; }
        public ICommand SignalRCommand { get; private set; }
        public ICommand EventSummaryCommand { get; private set; }
        public ICommand BadgeCommand { get; private set; }
        public ICommand SpeakerDinnerCommand { get; private set; }
        public ICommand QRCodeCommand { get; private set; }
        public ICommand TreasureCommand { get; private set; }
        public ICommand PollingCommand { get; private set; }

        public WebAPiCallServices webAPiCallServices;

        private GridLength _heightpara;

        public GridLength heightpara
        {
            get { return _heightpara; }
            set
            {
                _heightpara = value;
                OnPropertyChanged("heightpara");
            }
        }

        public Color FrameL1Bgcolor
        {
            get { return frameL1Bgcolor; }
            set
            {
                frameL1Bgcolor = value;
                OnPropertyChanged(nameof(FrameL1Bgcolor));
            }
        }
        private Color listFrame1BorderColor = Color.Transparent;
        public Color ListFrame1BorderColor
        {
            get { return listFrame1BorderColor; }
            set
            {
                listFrame1BorderColor = value;
                OnPropertyChanged(nameof(ListFrame1BorderColor));
            }
        }
        private Color programBacgroundColor = Color.FromHex("#FEEECA");
        public Color ProgramBacgroundColor
        {
            get { return programBacgroundColor; }
            set
            {
                programBacgroundColor = value;
                OnPropertyChanged(nameof(ProgramBacgroundColor));
            }
        }
        private Color programBorderColor = Color.Transparent;
        public Color ProgramBorderColor
        {
            get { return programBorderColor; }
            set
            {
                programBorderColor = value;
                OnPropertyChanged(nameof(ProgramBorderColor));
            }
        }

        private Color programL1Bgcolor = Color.FromHex("#FEEECA");
        public Color ProgramL1Bgcolor
        {
            get { return programL1Bgcolor; }
            set
            {
                programL1Bgcolor = value;
                OnPropertyChanged(nameof(ProgramL1Bgcolor));
            }
        }
        private Color programL1BorderColor = Color.Transparent;
        public Color ProgramL1BorderColor
        {
            get { return programL1BorderColor; }
            set
            {
                programL1BorderColor = value;
                OnPropertyChanged(nameof(ProgramL1BorderColor));
            }
        }
        public bool isListVisible
        {
            get { return _isListVisible; }
            set
            {
                _isListVisible = value;
                OnPropertyChanged(nameof(isListVisible));
            }
        }
        private bool _isSpeaker = false;
        public bool isSpeaker
        {
            get { return _isSpeaker; }
            set
            {
                _isSpeaker = value;
                OnPropertyChanged(nameof(isSpeaker));
            }
        }
        public bool isGridVisible
        {
            get { return _isGridVisible; }
            set
            {
                _isGridVisible = value;
                OnPropertyChanged(nameof(isGridVisible));
            }
        }
        public Color Item1BacgroundColor
        {
            get { return _item1BacgroundColor; }
            set
            {
                _item1BacgroundColor = value;
                OnPropertyChanged(nameof(Item1BacgroundColor));
            }
        }
        public Color Item1BorderColor
        {
            get { return _item1BorderColor; }
            set
            {
                _item1BorderColor = value;
                OnPropertyChanged(nameof(Item1BorderColor));
            }
        }
        private Color speakerBackgroundColor = Color.FromHex("#FEEECA");
        public Color SpeakerBackgroundColor
        {
            get { return speakerBackgroundColor; }
            set
            {
                speakerBackgroundColor = value;
                OnPropertyChanged(nameof(SpeakerBackgroundColor));
            }
        }

        private Color speakerBorderColor = Color.Transparent;
        public Color SpeakerBorderColor
        {
            get { return speakerBorderColor; }
            set
            {
                speakerBorderColor = value;
                OnPropertyChanged(nameof(SpeakerBorderColor));
            }
        }

        private Color organizationBackgroundColor = Color.FromHex("#FEEECA");
        public Color OrganizationBackgroundColor
        {
            get
            {
                return organizationBackgroundColor;
            }
            set
            {
                organizationBackgroundColor = value;
                OnPropertyChanged(nameof(OrganizationBackgroundColor));
            }
        }
        private Color organizationBorderColor = Color.Transparent;
        public Color OrganizationBorderColor
        {
            get { return organizationBorderColor; }
            set
            {
                OrganizationBorderColor = value;
                OnPropertyChanged(nameof(OrganizationBorderColor));
            }
        }
        private Color upcomingEventsBackgroundColor = Color.FromHex("#FEEECA");
        public Color UpcomingEventsBackgroundColor
        {
            get
            {
                return upcomingEventsBackgroundColor;
            }

            set
            {
                upcomingEventsBackgroundColor = value;
                OnPropertyChanged(nameof(UpcomingEventsBackgroundColor));
            }
        }
        private Color upcomingEventsBorderColor = Color.Transparent;
        public Color UpcomingEventsBorderColor
        {
            get
            {
                return upcomingEventsBorderColor;
            }

            set
            {
                upcomingEventsBorderColor = value;
                OnPropertyChanged(nameof(UpcomingEventsBorderColor));
            }
        }

        private Color eventTermAndConditionBackgroundColor = Color.FromHex("#FEEECA");
        public Color EventTermAndConditionBackgroundColor
        {
            get
            {
                return eventTermAndConditionBackgroundColor;
            }

            set
            {
                eventTermAndConditionBackgroundColor = value;
                OnPropertyChanged(nameof(EventTermAndConditionBackgroundColor));
            }
        }

        private Color eventTermAndConditionBorderColor = Color.Transparent;
        public Color EventTermAndConditionBorderColor
        {
            get
            {
                return eventTermAndConditionBorderColor;
            }

            set
            {
                eventTermAndConditionBorderColor = value;
                OnPropertyChanged(nameof(EventTermAndConditionBorderColor));
            }
        }
        private ImageSource _BannerImage;

        public ImageSource BannerImage
        {
            get { return _BannerImage; }
            set
            {
                _BannerImage = value;
                OnPropertyChanged(nameof(BannerImage));
            }
        }
        private string token;
        public string Token
        {
            get { return token; }
            set
            {
                token = value;
                OnPropertyChanged(nameof(Token));
            }
        }

        private bool _feedbacklabel;

        public bool feedbacklabel
        {
            get { return _feedbacklabel; }
            set
            {
                _feedbacklabel = value;
                OnPropertyChanged("feedbacklabel");
            }
        }
        public static bool flag;

        public HomePageViewModel()
        {
            flag = false;
            ExhibitorSurveyCommand = new Command(ExhibitorSurveyCommandHandler);
            SpeakerSurveyCommand = new Command(SpeakerSurveyCommandHandler);
            AttendeeCommand = new Command(AttendeeCommandHandler);
            CalendarCommand = new Command(CalendarCommandHandler);
            Item1Command = new Command(Item1Handler);
            ListCommnad = new Command(ListHandler);
            OpenMenuDrawer = new Command(OpenMenuDrawerHandler);
            FrameL1Command = new Command(FrameL1CommandHandler);
            ProgramL1Command = new Command(ProgramL1CommandHandler);
            ProgramCommand = new Command(ProgramCommandHandler);
            MyScheduleCommand = new Command(MyScheduleCommandHandler);
            SpeakerCommand = new Command(SpeakerCommandHandler);
            OrganizationCommand = new Command(OrganizationCommandHandler);
            EventsCommand = new Command(EventsCommandHandler);
            MyMeetingsCommand = new Command(MyMeetingsCommandHandler);
            EventContactCommand = new Command(EventContactCommandHandler);
            NetworkCommand = new Command(NetworkCommandHandler);
            TwitterCommand = new Command(TwitterCommandHandler);
            UpdateCommand = new Command(UpdateCommandHandler);
            FindPeopleCommand = new Command(FindPeopleCommandHandler);
            EventSummaryCommand = new Command(EventSummaryCommandHandler);
            BadgeCommand = new Command(BadgeCommandHandler);
            MyFeedbackCommand = new Command(MyFeedbackCommandHandler);
            NonAttendeeCommand = new Command(NonAttendeeCommandHandler);
            SpeakerDinnerCommand = new Command(SpeakerDinnerCommandHandler);
            QRCodeCommand = new Command(QRCodeCommandHandler);
            TreasureCommand = new Command(TreasureCommandhandler);
            PollingCommand = new Command(PollingCommandCommandhandler);
            ConnectCommand = new Command(async () => await Connect());
            //Countdown();
            FeedbackVisible = true;
            UserModel userModel = JsonConvert.DeserializeObject<UserModel>(Application.Current.Properties["User"].ToString());
            //bannerimagedownload();
            //DownloadImage(new Uri(userModel.Bannerimageurl));

            //BannerImage = ImageSource.FromUri(new Uri(userModel.Bannerimageurl));
            //feedback();
            //internet_connection();
            SignalRCommand = new Command(SignalRCommandHandler);
            Util.CalendarPermission = Preferences.Get("CalendarPermission", string.Empty);
            webAPiCallServices = new WebAPiCallServices();
            if (Util.IsSpeaker.ToLower() == "true")
            {
                isSpeaker = true;
            }
            //isSpeaker = true;

            if (Constant.IsProduction)
            {
                //IsDevelopment = false;
                IsDevelopment = true;
            }
            else
            {
                IsDevelopment = true;
            }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {

            }
            else
            {
                //BannerImage = ImageSource.FromUri(new Uri(userModel.Bannerimageurl));
                if (Util.BannerImagesource != null)//need work
                {
                    BannerImage = Util.BannerImagesource;
                }
                else
                {
                    DownloadImageAsync(new Uri(userModel.Bannerimageurl));
                }
                //chat status
                chaticonfunction();

                hubConnection = new HubConnection(Constant.SiteURL + "/signalr");
                chatHubProxy = hubConnection.CreateHubProxy("ChatHub");
                ConnectCommand.Execute(null);
                hubConnection.Error += HubConnection_Error;
                hubConnection.Reconnected += HubConnection_Reconnected;
                hubConnection.Closed += HubConnection_Closed;

                var mainPage = Application.Current.MainPage;
                if (mainPage is ChatReply ||
                    (mainPage is NavigationPage && ((NavigationPage)mainPage).CurrentPage is ChatReply))
                {
                }
                else
                {
                    chatHubProxy.On<string, string, string, string, int, string, string>("sendAsync", (id, user, image, message, chattype, uniqueID, grpName) =>
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            if (App.IsInForeground)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    if (flag == true)
                                    {
                                        return;
                                    }
                                    flag = true;
                                    if (!string.IsNullOrEmpty(message))
                                    {
                                        message = Regex.Replace(message, @"<[^>]*>", String.Empty).Trim();
                                        if (message.Contains("&rsquo;"))
                                        {
                                            message = message.Replace("&rsquo;", "'");
                                        }
                                        if (message.Contains("&amp;"))
                                        {
                                            message = message.Replace("&amp;", "&");
                                        }
                                        if (message.Contains("&ldquo;"))
                                        {
                                            message = message.Replace("&ldquo;", "'");
                                        }
                                        if (message.Contains("&nbsp;"))
                                        {
                                            message = message.Replace("&nbsp;", " ");
                                        }
                                        if (message.Contains("&rdquo;"))
                                        {
                                            message = message.Replace("&rdquo;", "'");
                                        }
                                        if (message.Contains("&lt;"))
                                        {
                                            message = message.Replace("&lt;", "<");
                                        }
                                        if (message.Contains("&gt;"))
                                        {
                                            message = message.Replace("&gt;", ">");
                                        }
                                        bool response = await Application.Current.MainPage.DisplayAlert("Message from " + user, message, "Reply", "Close");
                                        if (response)
                                        {
                                            await Application.Current.MainPage.Navigation.PushAsync(new Views.ChatReply(id, user, image, message, chattype, uniqueID, grpName));
                                        }
                                    }
                                });
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(message))
                                {
                                    message = Regex.Replace(message, @"<[^>]*>", String.Empty).Trim();
                                    if (message.Contains("&rsquo;"))
                                    {
                                        message = message.Replace("&rsquo;", "'");
                                    }
                                    if (message.Contains("&amp;"))
                                    {
                                        message = message.Replace("&amp;", "&");
                                    }
                                    if (message.Contains("&ldquo;"))
                                    {
                                        message = message.Replace("&ldquo;", "'");
                                    }
                                    if (message.Contains("&nbsp;"))
                                    {
                                        message = message.Replace("&nbsp;", " ");
                                    }
                                    if (message.Contains("&rdquo;"))
                                    {
                                        message = message.Replace("&rdquo;", "'");
                                    }
                                    if (message.Contains("&lt;"))
                                    {
                                        message = message.Replace("&lt;", "<");
                                    }
                                    if (message.Contains("&gt;"))
                                    {
                                        message = message.Replace("&gt;", ">");
                                    }
                                    DependencyService.Get<ILocalNotificationsService>().ShowNotification(id, user, image, message);
                                }

                            }
                        }
                    });
                }
            }


        }

        private async void TreasureCommandhandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TreasureHuntPage());
        }

        private async void PollingCommandCommandhandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PollingPage(new PollingResponseModel()));
        }

        private async void QRCodeCommandHandler(object obj)
        {
            var CurrentStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (CurrentStatus == PermissionStatus.Granted)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new MagiStandardsPage());
            }
            else 
            {
                PermissionStatus status = await Permissions.RequestAsync<Permissions.Camera>();
                if (status == PermissionStatus.Granted)
                {
                    await Task.Delay(500);
                    await Application.Current.MainPage.Navigation.PushAsync(new MagiStandardsPage());
                }
                else if (status == PermissionStatus.Unknown)
                {
                    await Task.Delay(500);
                    await Application.Current.MainPage.Navigation.PushAsync(new MagiStandardsPage());
                }
            }            
        }

        private async void SpeakerDinnerCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SpeakerDinnerPage());
        }

        private bool _ChatStatusBool;

        public bool ChatStatusBool
        {
            get { return _ChatStatusBool; }
            set
            {
                _ChatStatusBool = value;
                OnPropertyChanged("ChatStatusBool");
            }
        }


        private async void chaticonfunction()
        {
            try
            {
                var response = await webAPiCallServices.GetChatStatus();
                if (response != null)
                {
                    ChatStatusBool = response.chatallowed;
                    Util.ChatStatus = ChatStatusBool;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void BadgeCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.EventBadgesPage());
        }

        //private async void bannerimagedownload() //not in use(interface Ifileservice)
        //{
        //    try
        //    {
        //        UserModel userModel = JsonConvert.DeserializeObject<UserModel>(Xamarin.Forms.Application.Current.Properties["User"].ToString());
        //        var url = new Uri(userModel.Bannerimageurl);
        //        var httpClient = new HttpClient();
        //        var imageData = await httpClient.GetStreamAsync(url);
        //        DependencyService.Get<IFileService>().SavePicture("ImageName.gif", imageData, "imagesFolder");

        //    }
        //    catch(Exception ex)
        //    {
        //        var bbb = ex.Message;
        //    }
        //    //UserModel userModel = JsonConvert.DeserializeObject<UserModel>(Xamarin.Forms.Application.Current.Properties["User"].ToString());
        //    //var url = new Uri(userModel.Bannerimageurl);
        //    //var httpClient = new HttpClient();
        //    //var stream = await httpClient.GetStreamAsync(url);
        //    //string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sig.png");
        //    //using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        //    //{
        //    //    await stream.CopyToAsync(fileStream);
        //    //}
        //}

        public string fileimagepath;

        public async Task<string> DownloadImageAsync(Uri URL)
        {
            try
            {
                WebClient webClient = new WebClient();
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Images", "temp");
                string fileName = URL.ToString().Split('/').Last();
                fileName = fileName.Replace("58_", "");
                fileName = fileName.Replace("52_", "fiftytwo");
                fileName = fileName.Replace("53_", "fiftythree");
                fileName = fileName.Replace("54_", "fiftyfour");
                fileName = fileName.Replace("55_", "fiftyfive");
                string filePath = Path.Combine(folderPath, fileName);
                /*webClient.DownloadDataCompleted += (s, e) =>
                {
                    Directory.CreateDirectory(folderPath);
                    if (File.Exists(filePath))
                    {
                    }
                    else
                    {
                        File.WriteAllBytes(filePath, e.Result);
                    }
                };
                 webClient.DownloadDataAsync(URL); */

                await DownloadDataAsync(URL, folderPath, filePath);

                var w = new ImageSourceConverter().ConvertFromInvariantString(filePath);
                BannerImage = (ImageSource)w;
                Util.BannerImagesource = (ImageSource)w;
                fileimagepath = filePath;
                return filePath;
            }
            catch (Exception ex)
            {
                var mes = ex.Message;
                return mes;
            }
        }

        public static async Task<byte[]> DownloadDataAsync(Uri URL, string folderPath, string filePath)
        {
            WebClient client = new WebClient();
            byte[] data = await client.DownloadDataTaskAsync(URL);
            // ProcessData(data);
            Directory.CreateDirectory(folderPath);
            if (File.Exists(filePath))
            {
            }
            else
            {
                File.WriteAllBytes(filePath, data);
            }
            return data;
        }
        private async void internet_connection()
        {
            //var connect = Xamarin.Essentials.Connectivity.NetworkAccess;
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await Application.Current.MainPage.DisplayAlert("Notification", "No internet connection. Check/Fix and try again", "OK");
            }
        }

        private void HubConnection_Reconnected()
        {
            ConnectCommand.Execute(null);
        }

        private void HubConnection_Closed()
        {
            ConnectCommand.Execute(null);
        }

        private void HubConnection_Error(Exception obj)
        {
            ConnectCommand.Execute(null);
        }

        private void feedback()
        {
            //UserModel userModel = JsonConvert.DeserializeObject<UserModel>(Xamarin.Forms.Application.Current.Properties["User"].ToString());
            if (Util.ISEventFeedbackResponse == "0")
            {
                Device.StartTimer(TimeSpan.FromSeconds(15), () =>
                {
                    return feedbacklabel = true;
                });
            }
            else
            {
                feedbacklabel = false;
            }
        }


        private async void SignalRCommandHandler(object obj)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new Views.ChatReply("","","","",1,"",""));
            //await Application.Current.MainPage.Navigation.PushAsync(new Views.ChatPage());
            //await Application.Current.MainPage.Navigation.PushAsync(new Views.ChatHistory());
            if (ChatStatusBool == true)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Views.ChatHistory());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Chat is unavailable", "OK");
            }

        }

        private async void NonAttendeeCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.NotAttendeeSurvey());
        }

        private async void ExhibitorSurveyCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.ExhibitorSurvey());
        }

        private async void SpeakerSurveyCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.SpeakerSurvey());
        }

        private async void AttendeeCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.AttendeeSurvey());
        }

        private async void CalendarCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.SurveyPage());
            //await Application.Current.MainPage.Navigation.PushAsync(new Views.AttendeeSurvey());
        }

        private async void MyFeedbackCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.FeedbackFormPage());
        }

        private async void UpdateCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.UpdatePage());
        }
        private async void FindPeopleCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.FindPeople());
        }

        private async void EventSummaryCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.EventSummary());
        }
        private async void TwitterCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.SignalRPage());
        }

        private async void NetworkCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NetworkingPage());
        }

        private async void EventContactCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EventContactsPage());
        }

        public async void MyMeetingsCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MeetingsPage());
        }
        public async void EventsCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UpcomingEventPage());
        }
        public async void OrganizationCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new OrganizationsPage());
        }
        public async void SpeakerCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SpeakerViewPage());
        }
        private async void MyScheduleCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ViewMySchedule());
        }

        private async void ProgramL1CommandHandler(object obj)
        {
            if (ProgramL1BorderColor == Color.Transparent)
                ProgramL1BorderColor = Color.FromHex("#e68a00");
            else if (ProgramL1BorderColor == Color.FromHex("#e68a00"))
                ProgramL1BorderColor = Color.Transparent;
            if (ProgramL1Bgcolor == Color.FromHex("#FEEECA"))
                ProgramL1Bgcolor = Color.WhiteSmoke;
            else if (ProgramL1Bgcolor == Color.WhiteSmoke)
                ProgramL1Bgcolor = Color.FromHex("#FEEECA");
            await Application.Current.MainPage.Navigation.PushAsync(new ProgramViewPage());
        }

        private async void ProgramCommandHandler(object obj)
        {

            await Application.Current.MainPage.Navigation.PushAsync(new ProgramViewPage());
        }

        private async void FrameL1CommandHandler(object obj)
        {
            if (ListFrame1BorderColor == Color.Transparent)
                ListFrame1BorderColor = Color.FromHex("#e68a00");
            else if (ListFrame1BorderColor == Color.FromHex("#e68a00"))
                ListFrame1BorderColor = Color.Transparent;
            if (FrameL1Bgcolor == Color.FromHex("#FEEECA"))
                FrameL1Bgcolor = Color.WhiteSmoke;
            else if (FrameL1Bgcolor == Color.WhiteSmoke)
                FrameL1Bgcolor = Color.FromHex("#FEEECA");
            await Application.Current.MainPage.Navigation.PushAsync(new PartnersPage());
        }

        private void OpenMenuDrawerHandler(object obj)
        {
            Helper.IsMaster();
        }

        private void ListHandler(object obj)
        {
            if (isGridVisible == true)
            {
                isGridVisible = false;
                isListVisible = true;
            }
            else if (isListVisible == true)
            {
                isListVisible = false;
                isGridVisible = true;
            }

        }
        private async void Item1Handler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PartnersPage());
        }

        private void Countdown()
        {
            Device.StartTimer(TimeSpan.FromSeconds(45), () =>
            {
                return FeedbackVisible = true;
            });

        }


        private bool _FeedbackVisible;
        public bool FeedbackVisible
        {
            get { return _FeedbackVisible; }
            set
            {
                _FeedbackVisible = value;
                OnPropertyChanged(nameof(FeedbackVisible));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task Load()
        {
            try
            {
                var deviceId = Preferences.Get("my_deviceId", string.Empty);
                if (string.IsNullOrEmpty(deviceId))
                {
                    deviceId = Guid.NewGuid().ToString();
                    Preferences.Set("my_deviceId", deviceId);
                }
                var deviceName = DeviceInfo.Name;
                var version = DeviceInfo.VersionString;
                var platform = DeviceInfo.Platform;
                int Apptype;
                if (platform == DevicePlatform.Android)
                {
                    Apptype = 2;
                }
                else
                {
                    Apptype = 1;
                }
                string x = Util.CurrEvent_pkey;
                int eventid;
                int.TryParse(x, out eventid);

                VersionResponseModel versionResponseModel = new VersionResponseModel();
                GetVersionRequestedModel getVersionRequestedModel = new GetVersionRequestedModel();

                getVersionRequestedModel.EventId = eventid;
                getVersionRequestedModel.AppType = Apptype;
                getVersionRequestedModel.DeviceId = deviceId;
                getVersionRequestedModel.DeviceInfo = $"{deviceName}{platform}{version}";
                if (!string.IsNullOrEmpty(Util.token))
                {
                    getVersionRequestedModel.DeviceToken = Util.token;
                }
                else
                {
                    getVersionRequestedModel.DeviceToken = "";
                }
                getVersionRequestedModel.AccountId = Util.AccountPkey;
                getVersionRequestedModel.DeviceVersion = VersionTracking.CurrentVersion;

                var response = await webAPiCallServices.GetVersionApi(getVersionRequestedModel);
                await Task.CompletedTask;
                if (response != null)
                {
                    if (!string.IsNullOrEmpty(response.VersionNumber))
                    {
                        versionResponseModel.VersionNumber = response.VersionNumber;
                    }
                    if (!string.IsNullOrEmpty(response.VersionText))
                    {
                        versionResponseModel.VersionText = response.VersionText;
                    }
                }
                else
                {
                    versionResponseModel.VersionNumber = "00";
                    versionResponseModel.VersionText = "00";
                }
                string serverversionstring = versionResponseModel.VersionNumber.Replace(".", "");
                int serverversion = Convert.ToInt32(serverversionstring);

                string appversionstring = VersionTracking.CurrentVersion.Replace(".", "");
                int appversion = Convert.ToInt32(appversionstring);


                if (serverversion > appversion)
                {
                    bool action = await Application.Current.MainPage.DisplayAlert("Update Info", $" {versionResponseModel.VersionText}\n Version Number {versionResponseModel.VersionNumber}", "Update", "Later");
                    if (action == true)
                    {
                        if (platform == DevicePlatform.Android)
                        {
                            await Launcher.TryOpenAsync("https://play.google.com/store/apps/details?id=com.magi.event");
                        }
                        else if (platform == DevicePlatform.iOS)
                        {
                            await Launcher.TryOpenAsync("https://apps.apple.com/us/app/magi-event/id1588614727");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            if (Constant.IsShowToken)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Util.token))
                    {
                        bool tokencopy = await Application.Current.MainPage.DisplayAlert("Device Token for Notifications", Util.token, "Copy To Clipboard", "Cancel");
                        if (tokencopy == true)
                        {
                            await Clipboard.SetTextAsync(Util.token);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

    }
}
