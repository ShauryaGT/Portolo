using MAGIApp.Model;
using MAGIApp.Views;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace MAGIApp
{
    public partial class App : Xamarin.Forms.Application
    {

        public App(bool ShallNavigate, string id, string user, string image, string message)
        {
            var platform = DeviceInfo.Platform;
            if (platform == DevicePlatform.Android)
            {
                Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            }            
            InitializeComponent();
            try
            {
                
                if (Properties.ContainsKey("User"))
                {
                    UserModel userModel = JsonConvert.DeserializeObject<UserModel>(Properties["User"].ToString());
                    if (userModel != null)
                    {
                        Utils.Util.UserName = userModel.UserName;
                        if (userModel.isLogin == true)
                        {
                            if (userModel.Expiry > DateTime.Now)
                            {
                                Utils.Util.DisplayName = userModel.DisplayName;
                                Utils.Util.Userid = userModel.UserId;
                                Utils.Util.AccessToken = userModel.Token;
                                Utils.Util.CurrEvent_pkey = userModel.CurrEvent_pkey;
                                Utils.Util.AccountPkey = userModel.Account_pkey;
                                Utils.Util.Password = userModel.Password;
                                Utils.Util.isChecked = userModel.isChecked;
                                Utils.Util.isLogin = userModel.isLogin;
                                Utils.Util.Image = userModel.Image;
                                Utils.Util.EventName = userModel.EventName;
                                Utils.Util.eventtypeid = userModel.eventtypeid;
                                Utils.Util.Country = userModel.Country;
                                Utils.Util.State = userModel.State;
                                Utils.Util.State_pKey = userModel.State_pKey;
                                Utils.Util.city = userModel.City;
                                Utils.Util.User_Email = userModel.User_Email;
                                Utils.Util.OtherState = userModel.OtherState;
                                Utils.Util.eventuserstatusid = userModel.eventuserstatusid;
                                Utils.Util.EventStartDate = userModel.startdayofevent;
                                Utils.Util.EventEndDate = userModel.enddayofevent;
                                Utils.Util.RegionCode = userModel.RegionCode;
                                Utils.Util.Region = userModel.Region;
                                Utils.Util.TimeOffset = userModel.TimeOffset;
								Utils.Util.Expiry = userModel.Expiry;
                                Utils.Util.ISEventFeedbackResponse = userModel.ISEventFeedbackResponse;
                                Utils.Util.LocationTimeInterval = userModel.LocationTimeInterval;
                                Utils.Util.IsLicenseNumber = userModel.IsLicenseNumber;
                                Utils.Util.IsSpeaker = userModel.IsSpeaker;
                                if (ShallNavigate)
                                {
                                    MainPage = new NavigationPage(new UserMasterPage());
                                    Current.MainPage.Navigation.PushAsync(new ChatReply(id, user, image,message,1,"",""));
                                }
                                else
                                {
                                    MainPage = new NavigationPage(new UserMasterPage());
                                }
                            }
                            else
                            {
                                if (userModel.isChecked == true)
                                {
                                    Utils.Util.isChecked = userModel.isChecked;
                                    Utils.Util.Password = userModel.Password;
                                }
                                else
                                {
                                    Utils.Util.isChecked = userModel.isChecked;
                                }
                                if (Properties.ContainsKey("TwitterAuthorization"))
                                {
                                    Properties.Remove("TwitterAuthorization");
                                }
                                MainPage = new NavigationPage(new LoginPage());
                            }
                        }
                        else
                        {
                            if (userModel.isChecked == true)
                            {
                                Utils.Util.isChecked = userModel.isChecked;
                                Utils.Util.Password = userModel.Password;
                            }
                            else
                            {
                                Utils.Util.isChecked = userModel.isChecked;
                            }
                            if (Properties.ContainsKey("TwitterAuthorization"))
                            {
                                Properties.Remove("TwitterAuthorization");
                            }
                            MainPage = new NavigationPage(new LoginPage());
                        }
                    }
                    else
                    {
                        MainPage = new NavigationPage(new LoginPage());
                    }
                }
                else
                {
                    if (Properties.ContainsKey("TwitterAuthorization"))
                    {
                        Properties.Remove("TwitterAuthorization");
                    }
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
            catch
            {

            }
            
        }




        public static bool IsInForeground { get; set; } = false;

        protected override void OnStart()
        {
            IsInForeground = true;
        }

        protected override void OnSleep()
        {
            
            IsInForeground = false;
        }

        protected override void OnResume()
        {
            IsInForeground = true;
            if (Properties.ContainsKey("User"))
            {
                UserModel userModel = JsonConvert.DeserializeObject<UserModel>(Properties["User"].ToString());
                if (userModel != null)
                {
                    if (userModel.Expiry > DateTime.Now)
                    {

                    }
                    else
                    {
                        if (userModel.isChecked == true)
                        {
                            Utils.Util.isChecked = userModel.isChecked;
                            Utils.Util.Password = userModel.Password;
                        }
                        else
                        {
                            Utils.Util.isChecked = userModel.isChecked;
                        }
                        if (Properties.ContainsKey("TwitterAuthorization"))
                        {
                            Properties.Remove("TwitterAuthorization");
                        }
                        MainPage = new NavigationPage(new LoginPage());
                    }
                }
            }
        }
    }
}
