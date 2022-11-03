using System;
using System.ComponentModel;
using System.Windows.Input;
using MAGIApp.Helpers;
using MAGIApp.Model;
using MAGIApp.Model.RequestModel;
using MAGIApp.Services;
using MAGIApp.Utils;
using MAGIApp.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Essentials;
using MAGIApp.Model.ResponseModel;
using MAGIApp.Constants;
using System.Diagnostics;

namespace MAGIApp.ViewModel
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private string userName;
        private string password;
        public ICommand SignInCommand { get; private set; }
        public ICommand ForgotCommand { get; private set; }
        public ICommand EyeCommand { get; private set; }
        public WebAPiCallServices webAPiCallServices;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private bool _PasswordVisi;

        public bool PasswordVisi
        {
            get { return _PasswordVisi; }
            set
            {
                _PasswordVisi = value;
                OnPropertyChanged(nameof(PasswordVisi));
            }
        }

        public LoginPageViewModel()
        {
            EyeVisible = "eye.png";
            LoginEnabled = true;
            PasswordVisi = true;
            if (Util.isChecked == true)
            {
                Password = Util.Password;
                isChecked = true;
            }
            else
            {
                //isChecked = false;
                isChecked = true;
            }
            UserName = Util.UserName;
            SignInCommand = new Command(SignInHandler);
            ForgotCommand = new Command(ForgotCommandHandler);
            EyeCommand = new Command(EyeCommandHandler);
            webAPiCallServices = new WebAPiCallServices();
        }
        private string _EyeVisible;

        public string EyeVisible
        {
            get { return _EyeVisible; }
            set
            {
                _EyeVisible = value;
                OnPropertyChanged(nameof(EyeVisible));
            }
        }

        private void EyeCommandHandler(object obj)
        {
            if (PasswordVisi)
            {
                PasswordVisi = false;
                EyeVisible = "invisible.png";
            }
            else
            {
                PasswordVisi = true;
                EyeVisible = "eye.png";
            }
        }

        private async void ForgotCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());
        }

        private bool _isChecked;

        public bool isChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("isChecked");
            }
        }


        private bool _LoginEnabled;

        public bool LoginEnabled
        {
            get { return _LoginEnabled; }
            set { _LoginEnabled = value; }
        }

        private async void SignInHandler(object obj)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    Helper.ShowLoader("Please Wait");
                    LoginEnabled = true;
                    if (!string.IsNullOrEmpty(UserName))
                    {
                        if (!String.IsNullOrEmpty(Password))
                        {
                            if (Constant.UrlDetection == "Production")
                            {
                                TokenProductionRequestModel tokenProductionRequestModel = new TokenProductionRequestModel
                                {
                                    LoginName = UserName,
                                    Password = Password
                                };
                                var result = await webAPiCallServices.GetUserProductionAuth(tokenProductionRequestModel);
                                if (result.access_token != null)
                                {
                                    Util.eventuserstatusid = result.eventuserstatusid;
                                    tokenProductionRequestModel.eventuserstatusid = result.eventuserstatusid;
                                    if (Util.eventuserstatusid == "1" || Util.eventuserstatusid == "-2" || Util.eventuserstatusid == "-1")
                                    {

                                    }
                                    else
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        return;
                                    }
                                    Util.UserName = UserName;
                                    Util.DisplayName = result.username;
                                    Util.Userid = result.userid;
                                    Util.AccessToken = result.access_token;
                                    Util.isChecked = isChecked;
                                    Util.Password = Password;
                                    Util.isLogin = true;
                                    Util.eventtypeid = result.eventtypeid;
                                    Util.EventStartDate = result.EventStartDate;
                                    Util.EventEndDate = result.EventEndDate;
                                    Util.Region = result.Region;
                                    Util.RegionCode = result.RegionCode;
                                    Util.TimeOffset = result.TimeOffset;
                                    Util.LocationTimeInterval = result.LocationTimeInterval;
                                    Util.IsLicenseNumber = result.IsLicenseNumber;
                                    Util.IsSpeaker = result.IsSpeaker;
                                    var url = Constant.SiteURL + "/" + result.LeftBlockImage;


                                    if (!String.IsNullOrEmpty(result.eventid))
                                    {
                                        Util.CurrEvent_pkey = result.eventid;
                                    }
                                    else
                                    {
                                        Util.CurrEvent_pkey = "48";
                                    }
                                    Util.EventName = result.eventname;
                                    if (Application.Current.Properties.Equals(result.eventname))
                                    {
                                        splashText = Application.Current.Properties[result.eventname] as string;
                                    }
                                    UserDetailsResponseModel Userresult = await webAPiCallServices.GetUserDetails();
                                    Util.Country = Userresult[0].CountryID;
                                    Util.State = Userresult[0].StateID;
                                    Util.State_pKey = Userresult[0].State_pKey;
                                    Util.city = Userresult[0].City;
                                    Util.OtherState = Userresult[0].OtherState;
                                    Util.AccountPkey = Userresult[0].Account_pkey;
                                    Util.User_Email = Userresult[0].Email;
                                    Util.BannerImagesource = null;
                                    UserModel userModel = new UserModel();
                                    userModel.UserName = Util.UserName;
                                    userModel.Password = Util.Password;
                                    userModel.isLogin = Util.isLogin;
                                    userModel.DisplayName = result.username;
                                    userModel.Image = Userresult[0].Image;
                                    userModel.UserId = result.userid;
                                    userModel.Token = result.access_token;
                                    userModel.isChecked = Util.isChecked;
                                    userModel.Role = result.role;
                                    userModel.Account_pkey = Userresult[0].Account_pkey;
                                    userModel.CurrEvent_pkey = Util.CurrEvent_pkey;
                                    userModel.EventName = Util.EventName;
                                    userModel.eventtypeid = Util.eventtypeid;
                                    userModel.Country = Util.Country;
                                    userModel.State = Util.State;
                                    userModel.State_pKey = Util.State_pKey;
                                    userModel.City = Util.city;
                                    userModel.OtherState = Util.OtherState;
                                    userModel.User_Email = Util.User_Email;
                                    userModel.startdayofevent = result.EventStartDate;
                                    userModel.enddayofevent = result.EventEndDate;
                                    userModel.Bannerimageurl = url;
                                    userModel.Region = Util.Region;
                                    userModel.RegionCode = Util.RegionCode;
                                    userModel.TimeOffset = Util.TimeOffset;
                                    userModel.LocationTimeInterval = Util.LocationTimeInterval;
                                    userModel.IsLicenseNumber = Util.IsLicenseNumber;
                                    userModel.IsSpeaker = Util.IsSpeaker;
                                    userModel.Expiry = result.expires_in.HasValue ? DateTime.Now.AddSeconds(result.expires_in.Value) : DateTime.Now.AddDays(1);
                                    Util.Expiry = userModel.Expiry;
                                    Util.Image = Userresult[0].Image;
                                    string strModel = JsonConvert.SerializeObject(userModel);
                                    if (Application.Current.Properties.ContainsKey("User"))
                                        Application.Current.Properties["User"] = strModel;
                                    else
                                        Application.Current.Properties.Add("User", strModel);
                                    await Application.Current.SavePropertiesAsync();
                                    Application.Current.MainPage = new NavigationPage(new UserMasterPage());
                                    Helper.HideLoader();
                                    LoginEnabled = true;
                                    Helper.ShowToast("Welcome  " + Util.DisplayName);
                                }
                                else
                                {
                                    if (result.error_description.Contains("password"))
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        Helper.ShowAlert("Alert", "Enter valid password.");
                                    }
                                    else if (result.error_description.Contains("event"))
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        await Application.Current.MainPage.DisplayAlert("Message", result.error_description, "Ok");
                                    }
                                    else if (result.error_description.Contains("App"))
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        await Application.Current.MainPage.DisplayAlert("Message", result.error_description, "Ok");
                                    }
                                    else if (result.error_description.Contains("user name"))
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        //Helper.ShowAlert("Alert", "You are not authorize to login. Contact MAGI.");
                                        Helper.ShowAlert("Alert", "Enter valid username or email address.");
                                    }
                                    else
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        Helper.ShowAlert("Alert", "Contact MAGI for assistance in signing in.");
                                    }
                                    //Helper.HideLoader();
                                    //LoginEnabled = true;
                                    //Helper.ShowAlert("Alert", "Contact MAGI for assistance in signing in.");
                                }
                            }
                            else
                            {
                                TokenRequestModel tokenRequest = new TokenRequestModel
                                {
                                    grant_type = "password",
                                    username = UserName,
                                    password = Password,
                                };
                                var result = await webAPiCallServices.GetUserAuth(tokenRequest);
                                if (result.access_token != null)
                                {
                                    Util.UserName = UserName;
                                    Util.eventuserstatusid = result.eventuserstatusid;
                                    Util.DisplayName = result.username;
                                    Util.Userid = result.userid;
                                    Util.AccessToken = result.access_token;
                                    Util.isChecked = isChecked;
                                    Util.Password = Password;
                                    Util.isLogin = true;
                                    Util.eventtypeid = result.eventtypeid;
                                    Util.RegistrationLevel_Pkey = result.RegistrationLevel_Pkey;
                                    Util.EventStartDate = result.EventStartDate;
                                    Util.EventEndDate = result.EventEndDate;
                                    Util.Region = result.Region;
                                    Util.RegionCode = result.RegionCode;
                                    Util.TimeOffset = result.TimeOffset;
                                    Util.ISEventFeedbackResponse = result.ISEventFeedbackResponse;
                                    Util.LocationTimeInterval = result.LocationTimeInterval;
                                    Util.IsLicenseNumber = result.IsLicenseNumber;
                                    Util.IsSpeaker = result.IsSpeaker;
                                    Util.BannerImagesource = null;
                                    var url = Constant.SiteURL + "/" + result.LeftBlockImage;

                                    tokenRequest.eventuserstatusid = result.eventuserstatusid;

                                    if (Util.eventuserstatusid == "1" || Util.eventuserstatusid == "-2" || Util.eventuserstatusid == "-1")
                                    {

                                    }
                                    else
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        Helper.ShowAlert("Alert", "Contact MAGI for assistance in signing in.");
                                        return;
                                    }

                                    if (!String.IsNullOrEmpty(result.eventid))
                                    {
                                        Util.CurrEvent_pkey = result.eventid;
                                    }
                                    else
                                    {
                                        Util.CurrEvent_pkey = "48";
                                    }
                                    Util.EventName = result.eventname;
                                    if (Application.Current.Properties.Equals(result.eventname))
                                    {
                                        splashText = Application.Current.Properties[result.eventname] as string;
                                    }
                                    UserDetailsResponseModel Userresult = await webAPiCallServices.GetUserDetails();
                                    Util.Country = Userresult[0].CountryID;
                                    Util.State = Userresult[0].StateID;
                                    Util.State_pKey = Userresult[0].State_pKey;
                                    Util.city = Userresult[0].City;
                                    Util.OtherState = Userresult[0].OtherState;
                                    Util.AccountPkey = Userresult[0].Account_pkey;
                                    Util.User_Email = Userresult[0].Email;
                                    Util.Nickname = Userresult[0].Nickname;
                                    Util.Organization = Userresult[0].OrganizationID;
                                    Util.JobTittle = Userresult[0].Title;
                                    Util.Department = Userresult[0].Department;
                                    Util.Phone = Userresult[0].Phone;
                                    UserModel userModel = new UserModel();
                                    userModel.UserName = Util.UserName;
                                    userModel.Password = Util.Password;
                                    userModel.isLogin = Util.isLogin;
                                    userModel.DisplayName = result.username;
                                    userModel.Image = Userresult[0].Image;
                                    userModel.UserId = result.userid;
                                    userModel.Token = result.access_token;
                                    userModel.isChecked = Util.isChecked;
                                    userModel.Role = result.role;
                                    userModel.Account_pkey = Userresult[0].Account_pkey;
                                    userModel.CurrEvent_pkey = Util.CurrEvent_pkey;
                                    userModel.EventName = Util.EventName;
                                    userModel.eventtypeid = Util.eventtypeid;
                                    userModel.Country = Util.Country;
                                    userModel.State = Util.State;
                                    userModel.State_pKey = Util.State_pKey;
                                    userModel.City = Util.city;
                                    userModel.OtherState = Util.OtherState;
                                    userModel.User_Email = Util.User_Email;
                                    userModel.eventuserstatusid = Util.eventuserstatusid;
                                    userModel.startdayofevent = Util.EventStartDate;
                                    userModel.enddayofevent = Util.EventEndDate;
                                    userModel.Region = Util.Region;
                                    userModel.RegionCode = Util.RegionCode;
                                    userModel.TimeOffset = Util.TimeOffset;
                                    userModel.ISEventFeedbackResponse = Util.ISEventFeedbackResponse;
                                    userModel.Bannerimageurl = url;
                                    userModel.Nickname = Util.Nickname;
                                    userModel.Organization = Util.Organization;
                                    userModel.Title = Util.JobTittle;
                                    userModel.Department = Util.Department;
                                    userModel.Phone = Userresult[0].Phone;
                                    userModel.Email = Userresult[0].Email;
                                    userModel.LocationTimeInterval = Util.LocationTimeInterval;
                                    userModel.IsLicenseNumber = Util.IsLicenseNumber;
                                    userModel.IsSpeaker = Util.IsSpeaker;
                                    userModel.Expiry = result.expires_in.HasValue ? DateTime.Now.AddSeconds(result.expires_in.Value) : DateTime.Now.AddDays(1);
                                    Util.Expiry = userModel.Expiry;
                                    Util.Image = Userresult[0].Image;
                                    userModel.RegistrationLevel_Pkey = result.RegistrationLevel_Pkey;
                                    string strModel = JsonConvert.SerializeObject(userModel);
                                    if (Application.Current.Properties.ContainsKey("User"))
                                        Application.Current.Properties["User"] = strModel;
                                    else
                                        Application.Current.Properties.Add("User", strModel);
                                    await Application.Current.SavePropertiesAsync();
                                    Application.Current.MainPage = new NavigationPage(new UserMasterPage());
                                    Helper.HideLoader();
                                    LoginEnabled = true;
                                    Helper.ShowToast("Welcome  " + Util.DisplayName);
                                }
                                else
                                {
                                    if (result.error_description.Contains("password"))
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        Helper.ShowAlert("Alert", "Enter valid password.");
                                    }
                                     else if (result.error_description.Contains("event"))
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        await Application.Current.MainPage.DisplayAlert("Message", result.error_description, "Ok");
                                    }
                                    else if (result.error_description.Contains("App"))
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        await Application.Current.MainPage.DisplayAlert("Message", result.error_description, "Ok");
                                    }
                                    else
                                    {
                                        Helper.HideLoader();
                                        LoginEnabled = true;
                                        //Helper.ShowAlert("Alert", "You are not authorize to login. Contact MAGI.");
                                        Helper.ShowAlert("Alert", "Enter valid username or email address.");
                                    }

                                }
                            }
                            Helper.HideLoader();
                            LoginEnabled = true;
                        }
                        else
                        {
                            Helper.HideLoader();
                            LoginEnabled = true;
                            await Application.Current.MainPage.DisplayAlert("Alert", "Password should not be blank", "OK");
                        }
                    }
                    else
                    {
                        Helper.HideLoader();
                        LoginEnabled = true;
                        await Application.Current.MainPage.DisplayAlert("Alert", "Username should not be blank", "OK");
                    }
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Helper.HideLoader();
                    Util.Country = "";
                    Util.State = "";
                    Util.State_pKey = "";
                    Util.city = "";
                    Util.OtherState = "";
                    Util.AccountPkey = "";
                    Util.User_Email = "";
                    Util.Image = "";
                    await Application.Current.MainPage.DisplayAlert("Alert", "Sign-in timed out. Sign in again.", "OK");
                }
            }
            else
            {
                Helper.ShowAlert("Network Error", "No internet connection. Check/Fix and try again");
            }

        }

        private string _splashText;

        public string splashText
        {
            get { return _splashText; }
            set
            {
                _splashText = value;
                OnPropertyChanged("splashText");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}