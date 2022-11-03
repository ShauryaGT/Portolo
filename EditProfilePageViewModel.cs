using FFImageLoading.Cache;
using FFImageLoading.Forms;
using MAGIApp.Helpers;
using MAGIApp.Model.ResponseModel;
using MAGIApp.Services;
using MAGIApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Media;

namespace MAGIApp.ViewModel.ProfileViewModel
{

    public class EditProfilePageViewModel : INotifyPropertyChanged
    {
        private string userName;
        private string firstName;
        private string lastName;
        private string departmentLabel;
        private string zipcodeLabel;
        private string activationDate;
        private string userMailidLabel;
        private string nickNameLabel;
        private string phoneticNameLabel;
        private string signInNameLabel;
        private ImageSource userImg;
        public ICommand UpdateProfileDataCommand { get; private set; }
        public ICommand GoHomeCommand { get; private set; }
        public WebAPiCallServices webAPiCallServices;
        public ICommand GoBackCommand { get; private set; }
        private string middleName;
        string _file_choosen
        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;
                OnPropertyChanged(nameof(MiddleName));
            }
        }
        public string file_choosen
        {
            get { return _file_choosen; }
            set
            {
                _file_choosen = value;
                OnPropertyChanged(nameof(_file_choosen));
            }
        }
        private GridLength _Heightpara;

        public GridLength Heightpara
        {
            get { return _Heightpara; }
            set
            {
                _Heightpara = value;
                OnPropertyChanged("Heightpara");
            }
        }


        private string accountLabel;
        public string AccountLabel
        {
            get { return accountLabel; }
            set
            {
                accountLabel = value;
                OnPropertyChanged(nameof(AccountLabel));
            }
        }

        private string salutationLabel;
        public string SalutationLabel
        {
            get { return salutationLabel; }
            set
            {
                salutationLabel = value;
                OnPropertyChanged(nameof(SalutationLabel));
            }
        }

        private string jobTittleLabel;
        public string JobTittleLabel
        {
            get { return jobTittleLabel; }
            set
            {
                jobTittleLabel = value;
                OnPropertyChanged(nameof(JobTittleLabel));
            }
        }
        private string organizationLabel;
        public string OrganizationLabel
        {
            get { return organizationLabel; }
            set
            {
                organizationLabel = value;
                OnPropertyChanged(nameof(OrganizationLabel));
            }
        }
        private string phoneLabel;
        public string PhoneLabel
        {
            get { return phoneLabel; }
            set
            {
                phoneLabel = value;
                OnPropertyChanged(nameof(PhoneLabel));
            }
        }
        private string phoneLabel2;
        public string PhoneLabel2
        {
            get { return phoneLabel2; }
            set
            {
                phoneLabel2 = value;
                OnPropertyChanged(nameof(PhoneLabel2));
            }
        }
        private string countryLabel;
        public string CountryLabel
        {
            get { return countryLabel; }
            set
            {
                countryLabel = value;
                OnPropertyChanged(nameof(CountryLabel));
            }
        }
        private string address1Label;
        public string Address1Label
        {
            get { return address1Label; }
            set
            {
                address1Label = value;
                OnPropertyChanged(nameof(Address1Label));
            }
        }
        private string address2Label;
        public string Address2Label
        {
            get { return address2Label; }
            set
            {
                address2Label = value;
                OnPropertyChanged(nameof(Address2Label));
            }
        }
        private string stateLabel;
        public string StateLabel
        {
            get { return stateLabel; }
            set
            {
                stateLabel = value;
                OnPropertyChanged(nameof(StateLabel));
            }
        }
        private string timeZoneLabel;
        public string TimeZoneLabel
        {
            get { return timeZoneLabel; }
            set
            {
                timeZoneLabel = value;
                OnPropertyChanged(nameof(TimeZoneLabel));
            }
        }
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public ImageSource UserImg
        {
            get { return userImg; }
            set
            {
                userImg = value;
                OnPropertyChanged(nameof(UserImg));
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string DepartmentLabel
        {
            get { return departmentLabel; }
            set
            {
                departmentLabel = value;
                OnPropertyChanged(nameof(DepartmentLabel));
            }
        }
        public string ZipcodeLabel
        {
            get { return zipcodeLabel; }
            set
            {
                zipcodeLabel = value;
                OnPropertyChanged(nameof(ZipcodeLabel));
            }
        }

        public string NickNameLabel
        {
            get { return nickNameLabel; }
            set
            {
                nickNameLabel = value;
                OnPropertyChanged(nameof(NickNameLabel));
            }
        }

        public string PhoneticNameLabel
        {
            get { return phoneticNameLabel; }
            set
            {
                phoneticNameLabel = value;
                OnPropertyChanged(nameof(PhoneticNameLabel));
            }
        }

        public string SignInNameLabel
        {
            get { return signInNameLabel; }
            set
            {
                signInNameLabel = value;
                OnPropertyChanged(nameof(SignInNameLabel));
            }
        }
        public string ActivationDate
        {
            get { return activationDate; }
            set
            {
                activationDate = value;
                OnPropertyChanged(nameof(ActivationDate));
            }
        }
        public string UserMailidLabel
        {
            get { return userMailidLabel; }
            set
            {
                userMailidLabel = value;
                OnPropertyChanged(nameof(UserMailidLabel));
            }
        }
        private string userMailidLabel2;
        public string UserMailidLabel2
        {
            get { return userMailidLabel2; }
            set
            {
                userMailidLabel2 = value;
                OnPropertyChanged(nameof(UserMailidLabel2));
            }
        }
        private string _PersonalBio;

        public string PersonalBio
        {
            get { return _PersonalBio; }
            set
            {
                _PersonalBio = value;
                OnPropertyChanged(nameof(PersonalBio));
            }
        }

        private string _userRole;

        public string userRole
        {
            get { return _userRole; }
            set
            {
                _userRole = value;
                OnPropertyChanged(nameof(userRole));
            }
        }

        private string _AboutMe;

        public string AboutMe
        {
            get { return _AboutMe; }
            set
            {
                _AboutMe = value;
                OnPropertyChanged(nameof(AboutMe));
            }
        }
        private string _CityLabel;

        public string CityLabel
        {
            get { return _CityLabel; }
            set
            {
                _CityLabel = value;
                OnPropertyChanged(nameof(CityLabel));
            }
        }

        private string _PhoneTypeID2;

        public string PhoneTypeID2
        {
            get { return _PhoneTypeID2; }
            set
            {
                _PhoneTypeID2 = value;
                OnPropertyChanged(nameof(PhoneTypeID2));
            }
        }
        private string _PhoneTypeID;

        public string PhoneTypeID
        {
            get { return _PhoneTypeID; }
            set
            {
                _PhoneTypeID = value;
                OnPropertyChanged(nameof(PhoneTypeID));
            }
        }

        private string _SuffixLabel;

        public string SuffixLabel
        {
            get { return _SuffixLabel; }
            set
            {
                _SuffixLabel = value;
                OnPropertyChanged(nameof(SuffixLabel));
            }
        }

        private string _EmailLabel;

        public string EmailLabel
        {
            get { return _EmailLabel; }
            set
            {
                _EmailLabel = value;
                OnPropertyChanged(nameof(EmailLabel));
            }
        }

        private string _CcLabel;

        public string CcLabel
        {
            get { return _CcLabel; }
            set
            {
                _CcLabel = value;
                OnPropertyChanged(nameof(CcLabel));
            }
        }

        private string _CcLabel2;

        public string CcLabel2
        {
            get { return _CcLabel2; }
            set
            {
                _CcLabel2 = value;
                OnPropertyChanged(nameof(CcLabel2));
            }
        }

        private string extLabel;

        public string ExtLabel
        {
            get { return extLabel; }
            set
            {
                extLabel = value;
                OnPropertyChanged(nameof(ExtLabel));
            }
        }

        private string extLabel2;

        public string ExtLabel2
        {
            get { return extLabel2; }
            set
            {
                extLabel2 = value;
                OnPropertyChanged(nameof(ExtLabel2));
            }
        }

        private string degreeLabel;

        public string DegreeLabel
        {
            get { return degreeLabel; }
            set
            {
                degreeLabel = value;
                OnPropertyChanged(nameof(DegreeLabel));
            }
        }

        private string linkedInLabel;
        public string LinkedInLabel
        {
            get { return linkedInLabel; }
            set
            {
                linkedInLabel = value;
                OnPropertyChanged(nameof(LinkedInLabel));
            }
        }

        private string skypeLabel;
        public string SkypeLabel
        {
            get { return skypeLabel; }
            set
            {
                skypeLabel = value;
                OnPropertyChanged(nameof(SkypeLabel));
            }
        }

        private string licenseTypeLabel;
        public string LicenseTypeLabel
        {
            get { return licenseTypeLabel; }
            set
            {
                licenseTypeLabel = value;
                OnPropertyChanged(nameof(LicenseTypeLabel));
            }
        }

        private string licenseStateLabel;
        public string LicenseStateLabel
        {
            get { return licenseStateLabel; }
            set
            {
                licenseStateLabel = value;
                OnPropertyChanged(nameof(LicenseStateLabel));
            }
        }

        private string licenseNumberLabel;
        public string LicenseNumberLabel
        {
            get { return licenseNumberLabel; }
            set
            {
                licenseNumberLabel = value;
                OnPropertyChanged(nameof(LicenseNumberLabel));
            }
        }

        private string memberLabel;
        public string MemberLabel
        {
            get { return memberLabel; }
            set
            {
                memberLabel = value;
                OnPropertyChanged(nameof(MemberLabel));
            }
        }

        private string journalLabel;
        public string JournalLabel
        {
            get { return journalLabel; }
            set
            {
                journalLabel = value;
                OnPropertyChanged(nameof(JournalLabel));
            }
        }

        private string allowCallLabel;
        public string AllowCallLabel
        {
            get { return allowCallLabel; }
            set
            {
                allowCallLabel = value;
                OnPropertyChanged(nameof(AllowCallLabel));
            }
        }

        public EditProfilePageViewModel()
        {
            UpdateProfileDataCommand = new Command(UpdateProfileDataCommandHandler);
            GoBackCommand = new Command(GoBackCommandHandler);
            GoHomeCommand = new Command(GoHomeTapped);
            webAPiCallServices = new WebAPiCallServices();
            LoadApi();
        }

        private List<EmailResponseModel> _emailResponseModels;

        public List<EmailResponseModel> emailResponseModels
        {
            get { return _emailResponseModels; }
            set 
            { 
                _emailResponseModels = value;
                OnPropertyChanged("emailResponseModels");
            }
        }

        private EmailResponseModel _emailResponseModel;

        public EmailResponseModel emailResponseModel
        {
            get { return _emailResponseModel; }
            set 
            { 
                _emailResponseModel = value;
                OnPropertyChanged("emailResponseModel");
            }
        }

        private List<EmailResponseModel> _emailResponseModels1;

        public List<EmailResponseModel> emailResponseModels1
        {
            get { return _emailResponseModels1; }
            set
            {
                _emailResponseModels1 = value;
                OnPropertyChanged("emailResponseModels1");
            }
        }

        private EmailResponseModel _emailResponseModel1;

        public EmailResponseModel emailResponseModel1
        {
            get { return _emailResponseModel1; }
            set
            {
                _emailResponseModel1 = value;
                OnPropertyChanged("emailResponseModel1");
            }
        }

        private List<PhoneResponseModel> _phoneResponseModels;

        public List<PhoneResponseModel> phoneResponseModels
        {
            get { return _phoneResponseModels; }
            set
            {
                _phoneResponseModels = value;
                OnPropertyChanged(nameof(phoneResponseModels));
            }
        }
        private List<PhoneResponseModel> _phoneResponseModels1;

        public List<PhoneResponseModel> phoneResponseModels1
        {
            get { return _phoneResponseModels1; }
            set
            {
                _phoneResponseModels1 = value;
                OnPropertyChanged(nameof(phoneResponseModels1));
            }
        }
        private PhoneResponseModel _phoneResponseModel;

        public PhoneResponseModel phoneResponseModel
        {
            get { return _phoneResponseModel; }
            set
            {
                _phoneResponseModel = value;
                OnPropertyChanged(nameof(phoneResponseModel));
            }
        }
        private PhoneResponseModel _phoneResponseModel1;

        public PhoneResponseModel phoneResponseModel1
        {
            get { return _phoneResponseModel1; }
            set
            {
                _phoneResponseModel1 = value;
                OnPropertyChanged(nameof(phoneResponseModel1));
            }
        }

        private List<DdlList> _SalutationList;

        public List<DdlList> SalutationList
        {
            get { return _SalutationList; }
            set
            {
                _SalutationList = value;
                OnPropertyChanged(nameof(SalutationList));
            }
        }

        private DdlList _SalutationModel;

        public DdlList SalutationModel1
        {
            get { return _SalutationModel; }
            set
            {
                _SalutationModel = value;
                OnPropertyChanged(nameof(SalutationModel1));
            }
        }

        private List<DdlList> _SuffixList;

        public List<DdlList> SuffixList
        {
            get { return _SuffixList; }
            set
            {
                _SuffixList = value;
                OnPropertyChanged(nameof(SuffixList));
            }
        }

        private DdlList _SuffixModel;

        public DdlList SuffixModel1
        {
            get { return _SuffixModel; }
            set
            {
                _SuffixModel = value;
                OnPropertyChanged(nameof(SuffixModel1));
            }
        }

        private List<DdlList> _EmailList;

        public List<DdlList> EmailList
        {
            get { return _EmailList; }
            set
            {
                _EmailList = value;
                OnPropertyChanged(nameof(EmailList));
            }
        }

        private DdlList _EmailModel;

        public DdlList EmailModel1
        {
            get { return _EmailModel; }
            set
            {
                _EmailModel = value;
                OnPropertyChanged(nameof(EmailModel1));
            }
        }

        private List<DdlList> _LicenseList;

        public List<DdlList> LicenseList
        {
            get { return _LicenseList; }
            set
            {
                _LicenseList = value;
                OnPropertyChanged(nameof(LicenseList));
            }
        }

        private DdlList _LicenseModel;

        public DdlList LicenseModel1
        {
            get { return _LicenseModel; }
            set
            {
                _LicenseModel = value;
                OnPropertyChanged(nameof(LicenseModel1));
            }
        }

        private List<CountryModel> _CountryList;

        public List<CountryModel> CountryList
        {
            get { return _CountryList; }
            set
            {
                _CountryList = value;
                OnPropertyChanged(nameof(CountryList));
            }
        }

        private CountryModel _CountryModel;

        public CountryModel CountryModel1
        {
            get { return _CountryModel; }
            set
            {
                _CountryModel = value;
                OnPropertyChanged(nameof(CountryModel1));

                if (CountryModel1 != null)
                {
                    StateTimeLoader();
                }
            }
        }


        private bool _StateVisible;

        public bool StateVisible
        {
            get { return _StateVisible; }
            set
            {
                _StateVisible = value;
                OnPropertyChanged(nameof(StateVisible));
            }
        }


        private List<StateModel> _StateList;

        public List<StateModel> StateList
        {
            get { return _StateList; }
            set
            {
                _StateList = value;
                OnPropertyChanged(nameof(StateList));
            }
        }

        private StateModel _StateModel;

        public StateModel StateModel1
        {
            get { return _StateModel; }
            set
            {
                _StateModel = value;
                OnPropertyChanged(nameof(StateModel1));
            }
        }

        private List<TimezoneModel> _TimezoneList;

        public List<TimezoneModel> TimezoneList
        {
            get { return _TimezoneList; }
            set
            {
                _TimezoneList = value;
                OnPropertyChanged(nameof(TimezoneList));
            }
        }

        private TimezoneModel _TimezoneModel;

        public TimezoneModel TimezoneModel1
        {
            get { return _TimezoneModel; }
            set
            {
                _TimezoneModel = value;
                OnPropertyChanged(nameof(TimezoneModel1));
            }
        }

        private bool _LabelStateVisible;

        public bool LabelStateVisible
        {
            get { return _LabelStateVisible; }
            set
            {
                _LabelStateVisible = value;
                OnPropertyChanged(nameof(LabelStateVisible));
            }
        }


        public List<DdlList> salutationResponseList;
        public List<DdlList> suffixResponseList;
        public List<DdlList> emailResponseList;
        public List<DdlList> LicenseResponseList;
        public List<PhoneResponseModel> phoneResponseList;
        public List<EmailResponseModel> EmailResList;
        public List<CountryModel> countryResponseList;
        public List<StateModel> stateResponseList;
        public List<TimezoneModel> timezoneResponseList;
        public List<UList> uList;
        private async void StateTimeLoader()
        {
            Helper.ShowLoader("Loading");
            try
            {
                var response = await webAPiCallServices.GetStateList(CountryModel1.pKey);
                if (response.Count == 0)
                {
                    StateVisible = false;
                    LabelStateVisible = true;
                    StateList = null;
                }
                else
                {
                    StateVisible = true;
                    LabelStateVisible = false;
                    stateResponseList = new List<StateModel>();
                    foreach (var data in response)
                    {
                        stateResponseList.Add(data);
                    }
                    StateList = new List<StateModel>();
                    StateList = stateResponseList.OrderBy(x => x.strText).ToList();
                    if (!string.IsNullOrEmpty(StateLabel))
                    {
                        StateModel1 = StateList.Where(x => x.strText == StateLabel).FirstOrDefault();
                    }
                }

            }
            catch
            {

            }
            try
            {
                var response = await webAPiCallServices.GetTimezoneList(CountryModel1.pKey);
                timezoneResponseList = new List<TimezoneModel>();
                foreach (var data in response)
                {
                    timezoneResponseList.Add(data);
                }
                TimezoneList = new List<TimezoneModel>();
                TimezoneList = timezoneResponseList.OrderBy(x => x.strText).ToList();
                if (!string.IsNullOrEmpty(TimeZoneLabel))
                {
                    TimezoneModel1 = TimezoneList.Where(x => x.strText == TimeZoneLabel).FirstOrDefault();
                }
            }
            catch
            {

            }
            Helper.HideLoader();
        }
        private async void LoadApi()
        {
            Helper.ShowLoader("Loading");
            try
            {
                await CachedImage.InvalidateCache(UserImg, CacheType.All, true);
                file_choosen = "No file chosen";
                UserImg = new FileImageSource();
                string imageUrl = Constants.Constant.SiteURL + "/" + Util.Image.Replace("~", "");
                //UserImg = ImageSource.FromUri(new Uri(imageUrl));
                //File file = ImageSource.FromResource()
                UserImg = ImageSource.FromResource(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures), "elimar-profile.jpg"));                                
                var response = await webAPiCallServices.GetSalutationListApi();
                salutationResponseList = response;
                SalutationList = new List<DdlList>();

                  salutationResponseList.Add(
                      new DdlList
                      {
                          strText = " --Select--",
                          pKey = ""
                      });  
                SalutationList = salutationResponseList.OrderBy(x => x.strText).ToList();
                if (!string.IsNullOrEmpty(SalutationLabel))
                {
                    SalutationModel1 = SalutationList.Where(x => x.strText == SalutationLabel).FirstOrDefault();
                }
            }
            catch
            {

            }
            try
            {
                var response = await webAPiCallServices.GetSuffixListApi();
                suffixResponseList = response;
                SuffixList = new List<DdlList>();
                SuffixList = suffixResponseList.OrderBy(x => x.strText).ToList();
                if (!string.IsNullOrEmpty(SuffixLabel))
                {
                    SuffixModel1 = SuffixList.Where(x => x.strText == SuffixLabel).FirstOrDefault();
                }
            }
            catch
            {

            }
            try
            {
                emailResponseList = SelectEmailList.EmailDDList;
                EmailList = new List<DdlList>();
                EmailList = emailResponseList.OrderBy(x => x.strText).ToList();
                if (!string.IsNullOrEmpty(EmailLabel))
                {
                    EmailModel1 = EmailList.Where(x => x.strText == EmailLabel).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

            }
            try
            {
                var response = await webAPiCallServices.GetLicenseListApi();
                LicenseResponseList = response;
                LicenseList = new List<DdlList>();
                LicenseList = LicenseResponseList.OrderBy(x => x.strText).ToList();
                if (!string.IsNullOrEmpty(LicenseTypeLabel))
                {
                    LicenseModel1 = LicenseList.Where(x => x.strText == LicenseTypeLabel).FirstOrDefault();
                }
            }
            catch
            {

            }
            try
            {
                var response = await webAPiCallServices.EmailTypesApi();
                EmailResList = new List<EmailResponseModel>();
                foreach(var data in response)
                {
                    EmailResList.Add(data);
                }
            }
            catch
            {

            }
            emailResponseModels = EmailResList.OrderBy(x => x.pKey).ToList();
            emailResponseModels1 = EmailResList.OrderBy(x => x.pKey).ToList();
            emailResponseModel = emailResponseModels.Where(x => x.strText == "Email").FirstOrDefault();
            emailResponseModel1 = emailResponseModels1.Where(x => x.strText == "Email").FirstOrDefault();
            try
            {
                var response = await webAPiCallServices.PhoneTypesApi();
                phoneResponseList = new List<PhoneResponseModel>();
                foreach (var data in response)
                {
                    phoneResponseList.Add(data);
                }
            }
            catch
            {

            }
            phoneResponseModels = phoneResponseList.OrderBy(x => x.PhoneTypeID).ToList();
            phoneResponseModels1 = phoneResponseList.OrderBy(x => x.PhoneTypeID).ToList();
            if (!string.IsNullOrEmpty(PhoneTypeID))
            {
                phoneResponseModel = phoneResponseModels.Where(x => x.PhoneTypeID == PhoneTypeID).FirstOrDefault();
            }
            if (!string.IsNullOrEmpty(PhoneTypeID2))
            {
                phoneResponseModel1 = phoneResponseModels1.Where(x => x.PhoneTypeID == PhoneTypeID2).FirstOrDefault();
            }
            try
            {
                var response = await webAPiCallServices.GetCountryList();
                countryResponseList = new List<CountryModel>();
                foreach (var data in response)
                {
                    countryResponseList.Add(data);
                }
                CountryList = new List<CountryModel>();
                CountryList = countryResponseList.ToList();
                if (!string.IsNullOrEmpty(CountryLabel))
                {
                    CountryModel1 = CountryList.Where(x => x.strText == CountryLabel).FirstOrDefault();
                }
            }
            catch
            {

            }
            Helper.HideLoader();
        }

        private async void GoBackCommandHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void GoHomeTapped(object obj)
        {
            Application.Current.MainPage = new NavigationPage(new Views.UserMasterPage());
        }
        private async void UpdateProfileDataCommandHandler(object obj)
        {
            if (!string.IsNullOrEmpty(UserMailidLabel))
            {
                bool isEmail = Regex.IsMatch(UserMailidLabel, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please fill the correct email id.", "Ok");
                    return;
                }
                else
                {
                    if (UserMailidLabel != UserDetails.Email)
                    {
                        var response = await webAPiCallServices.IsVerifyUsedEmail(UserMailidLabel);
                        if(response.ISUsed)
                        {
                            await Application.Current.MainPage.DisplayAlert("Check Email Id", "Your first email is already associated with some other account", "OK");
                            return;
                        }
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please fill in your email id.", "Ok");
                return;
            }
            if (!string.IsNullOrEmpty(UserMailidLabel2))
            {
                bool isEmail2 = Regex.IsMatch(UserMailidLabel2, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!isEmail2)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please fill the correct email id.", "Ok");
                    return;
                }
                else
                {
                    if (UserMailidLabel2 != UserDetails.Email2)
                    {
                        var response = await webAPiCallServices.IsVerifyUsedEmail(UserMailidLabel2);
                        if (response.ISUsed || UserMailidLabel2 == UserMailidLabel)
                        {
                            await Application.Current.MainPage.DisplayAlert("Check Email Id", "Second email duplicates first email or email of alternate contact person", "OK");
                            return;
                        }
                    }
                }
            }
            if(string.IsNullOrEmpty(phoneLabel))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please fill the phone number.", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please fill your first name.", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please fill your last name.", "Ok");
                return;
            }
            try
            {
                Helper.ShowLoader("Updating profile");
                UserDetails.ZipCode = ZipcodeLabel;
                UserDetails.Firstname = FirstName;
                UserDetails.MiddleName = MiddleName;
                UserDetails.Lastname = LastName;
                UserDetails.Title = jobTittleLabel.Replace("&amp;", "");
                UserDetails.PersonalBio = PersonalBio;
                UserDetails.userRole = userRole;
                UserDetails.AboutMe = AboutMe;
                UserDetails.Phone = PhoneLabel;
                UserDetails.Phone2 = PhoneLabel2;
                UserDetails.Address1 = Address1Label;
                UserDetails.Address2 = Address2Label;
                UserDetails.Email = UserMailidLabel;
                UserDetails.Email2 = UserMailidLabel2;
                UserDetails.Nickname = NickNameLabel;
                UserDetails.PhoneticName = PhoneticNameLabel;
                UserDetails.UL = SignInNameLabel;
                UserDetails.Phone1CC = CcLabel;
                UserDetails.Phone2CC = CcLabel2;
                UserDetails.Phone1Ext = ExtLabel;
                UserDetails.Phone2Ext = ExtLabel2;
                UserDetails.Degrees = DegreeLabel;
                UserDetails.LinkedInProfile = LinkedInLabel;
                UserDetails.SkypeAddress = SkypeLabel;
                UserDetails.LicenseNumber = LicenseNumberLabel;
                UserDetails.LicenseState = LicenseStateLabel;
                UserDetails.Member = MemberLabel;
                UserDetails.Suffix_pkey = SuffixLabel;
                UserDetails.GetJournal = JournalLabel;
                UserDetails.EmailUsed = EmailLabel;
                if (phoneResponseModel != null)
                {
                    UserDetails.PhoneTypeID = phoneResponseModel.PhoneTypeID;
                    UserDetails.PhoneType_pKey = phoneResponseModel.pKey;
                }
                if (phoneResponseModel1 != null)
                {
                    UserDetails.PhoneTypeID2 = phoneResponseModel1.PhoneTypeID;
                    UserDetails.PhoneType2_pKey = phoneResponseModel1.pKey;
                }

                UserDetails.City = CityLabel;
                UserDetails.Department = DepartmentLabel;
                if (StateModel1 == null)
                {
                    UserDetails.State_pKey = null;
                    UserDetails.OtherState = StateLabel;
                }
                else
                {
                    if (StateList != null)
                    {
                        if (StateList.Count > 0)
                        {
                            UserDetails.State_pKey = StateModel1.pKey;
                            UserDetails.OtherState = null;
                        }
                        else
                        {
                            UserDetails.State_pKey = null;
                            UserDetails.OtherState = StateLabel;
                        }
                    }
                }
                if (TimezoneModel1 == null)
                {
                    UserDetails.TimezonePKey = null;
                }
                else
                {
                    UserDetails.TimezonePKey = TimezoneModel1.pKey;
                }
                //var uList = new UList();
                //uList.Department = UserDetails.Department;
                //uList.MiddleName = UserDetails.MiddleName;
                //uList.OtherState = UserDetails.OtherState;
                //uList.PhoneType_pKey = UserDetails.PhoneType_pKey;
                //uList.PhoneType2_pKey = UserDetails.PhoneType2_pKey;
                //uList.AboutMe = UserDetails.AboutMe;
                //uList.accountstatusID = UserDetails.accountstatusID;
                //uList.AccountStatus_pkey = UserDetails.AccountStatus_pkey;
                //uList.Account_pkey = UserDetails.Account_pkey;
                //uList.Activated = UserDetails.Activated;
                //uList.ActivationDate = UserDetails.ActivationDate;
                //uList.Address1 = UserDetails.Address1;
                //uList.Address2 = UserDetails.Address2;
                //uList.CountryID = UserDetails.CountryID;
                //uList.Country_pKey = CountryModel1.pKey;
                //uList.AllowCall = UserDetails.AllowCall;
                //uList.AllowEmail = UserDetails.AllowEmail;
                //uList.AllowText = UserDetails.AllowText;
                //uList.City = UserDetails.City;
                //uList.Comment = UserDetails.Comment;
                //uList.ContactName = UserDetails.ContactName;
                //uList.Degrees = UserDetails.Degrees;
                //uList.Email = UserDetails.Email;
                //uList.Email2 = UserDetails.Email2;
                //uList.Firstname = UserDetails.Firstname;
                //uList.GeneralInterestInBeingSpeaker = UserDetails.GeneralInterestInBeingSpeaker;
                //uList.HasPhoto = UserDetails.HasPhoto;
                //uList.Image = UserDetails.Image;
                //uList.LastApprovedDate = UserDetails.LastApprovedDate;
                //uList.LastLogin = UserDetails.LastLogin;
                //uList.Lastname = UserDetails.Lastname;
                //uList.LastProfileUpdate = UserDetails.LastProfileUpdate;
                //uList.LastSpeakerProfileUpdate = UserDetails.LastSpeakerProfileUpdate;
                //uList.LastViewedEvent_pKey = UserDetails.LastViewedEvent_pKey;
                //uList.MostRecentExternalAccess = UserDetails.MostRecentExternalAccess;
                //uList.Phone1CC = UserDetails.Phone1CC;
                //uList.Nickname = UserDetails.Nickname;
                //uList.OrganizationID = UserDetails.OrganizationID;
                //uList.OrganizationTypeID = UserDetails.OrganizationTypeID;
                //uList.OrganizationType_pKey = UserDetails.OrganizationType_pKey;
                //uList.PersonalBio = UserDetails.PersonalBio;
                //uList.Phone = UserDetails.Phone;
                //uList.Phone1Ext = UserDetails.Phone1Ext;
                //uList.Phone2 = UserDetails.Phone2;
                //uList.Phone2CC = UserDetails.Phone2CC;
                //uList.Phone2Ext = UserDetails.Phone2Ext;
                //uList.PhoneticName = UserDetails.PhoneticName;
                //uList.PhotoApproved = UserDetails.PhotoApproved;
                //uList.PrioritySpeaker = UserDetails.PrioritySpeaker;
                //uList.SalutationID = UserDetails.SalutationID;
                //uList.SkypeAddress = UserDetails.SkypeAddress;
                //uList.StaffMember = UserDetails.StaffMember;
                //uList.StateID = UserDetails.StateID;
                //uList.State_pKey = UserDetails.State_pKey;
                //uList.TimezonePKey = UserDetails.TimezonePKey;
                //uList.Title = UserDetails.Title;
                //uList.URL = UserDetails.URL;
                //uList.VIP = UserDetails.VIP;
                //uList.ZipCode = UserDetails.ZipCode;
                //uList.PhoneTypeID = UserDetails.PhoneTypeID;
                //uList.PhoneTypeID2 = UserDetails.PhoneTypeID2;
                //uList.Member = UserDetails.Member;
                //uList.LinkedInProfile = UserDetails.LinkedInProfile;
                //uList.showJournal = UserDetails.showJournal;
                //uList.TimeZone = UserDetails.TimeZone;
                //uList.strEmailUsed = UserDetails.strEmailUsed;
                //uList.UL = UserDetails.UL;
                //uList.ParentOrganization_pKey = UserDetails.ParentOrganization_pKey;
                //uList.Salutation_pKey = SalutationModel1.pKey;
                //uList.Suffix_pkey = SuffixModel1.strText;
                //uList.Timezone = UserDetails.Timezone;
                //uList.Suffix = SuffixModel1.strText;
                //uList.GetJournal = UserDetails.GetJournal;
                //    uList.EmailUsed = UserDetails.EmailUsed;

                if(SalutationModel1 != null)
                {
                    if (string.IsNullOrEmpty(SalutationModel1.pKey))
                    {
                        SalutationModel1 = null;
                    }
                }

                UList uList = new UList()
                {
                    Department = UserDetails.Department ?? string.Empty,
                    MiddleName = UserDetails.MiddleName ?? string.Empty,
                    OtherState = UserDetails.OtherState ?? string.Empty,
                    PhoneType_pKey = UserDetails.PhoneType_pKey ?? string.Empty,
                    PhoneType2_pKey = UserDetails.PhoneType2_pKey ?? string.Empty,
                    AboutMe = UserDetails.AboutMe ?? string.Empty,
                    accountstatusID = UserDetails.accountstatusID ?? string.Empty,
                    AccountStatus_pkey = UserDetails.AccountStatus_pkey ?? string.Empty,
                    Account_pkey = UserDetails.Account_pkey ?? string.Empty,
                    Activated = UserDetails.Activated ?? string.Empty,
                    ActivationDate = UserDetails.ActivationDate ?? string.Empty,
                    Address1 = UserDetails.Address1 ?? string.Empty,
                    Address2 = UserDetails.Address2 ?? string.Empty,
                    CountryID = UserDetails.CountryID ?? string.Empty,
                    Country_pKey = CountryModel1 != null ? CountryModel1.pKey : string.Empty,
                    AllowCall = UserDetails.AllowCall ?? string.Empty,
                    AllowEmail = UserDetails.AllowEmail ?? string.Empty,
                    AllowText = UserDetails.AllowText ?? string.Empty,
                    City = UserDetails.City ?? string.Empty,
                    Comment = UserDetails.Comment ?? string.Empty,
                    ContactName = UserDetails.ContactName ?? string.Empty,
                    Degrees = UserDetails.Degrees ?? string.Empty,
                    Email = UserDetails.Email ?? string.Empty,
                    Email2 = UserDetails.Email2 ?? string.Empty,
                    Firstname = UserDetails.Firstname ?? string.Empty,
                    GeneralInterestInBeingSpeaker = UserDetails.GeneralInterestInBeingSpeaker ?? string.Empty,
                    HasPhoto = UserDetails.HasPhoto ?? string.Empty,
                    Image = UserDetails.Image ?? string.Empty,
                    LastApprovedDate = UserDetails.LastApprovedDate ?? string.Empty,
                    LastLogin = UserDetails.LastLogin ?? string.Empty,
                    Lastname = UserDetails.Lastname ?? string.Empty,
                    LastProfileUpdate = UserDetails.LastProfileUpdate ?? string.Empty,
                    LastSpeakerProfileUpdate = UserDetails.LastSpeakerProfileUpdate ?? string.Empty,
                    LastViewedEvent_pKey = UserDetails.LastViewedEvent_pKey ?? string.Empty,
                    MostRecentExternalAccess = UserDetails.MostRecentExternalAccess ?? string.Empty,
                    Phone1CC = UserDetails.Phone1CC ?? string.Empty,
                    Nickname = UserDetails.Nickname ?? string.Empty,
                    OrganizationID = UserDetails.OrganizationID ?? string.Empty,
                    OrganizationTypeID = UserDetails.OrganizationTypeID ?? string.Empty,
                    OrganizationType_pKey = UserDetails.OrganizationType_pKey ?? string.Empty,
                    PersonalBio = UserDetails.PersonalBio ?? string.Empty,
                    Phone = UserDetails.Phone ?? string.Empty,
                    Phone1Ext = UserDetails.Phone1Ext ?? string.Empty,
                    Phone2 = UserDetails.Phone2 ?? string.Empty,
                    Phone2CC = UserDetails.Phone2CC ?? string.Empty,
                    Phone2Ext = UserDetails.Phone2Ext ?? string.Empty,
                    PhoneticName = UserDetails.PhoneticName ?? string.Empty,
                    PhotoApproved = UserDetails.PhotoApproved ?? string.Empty,
                    PrioritySpeaker = UserDetails.PrioritySpeaker ?? string.Empty,
                    SalutationID = UserDetails.SalutationID ?? string.Empty,
                    SkypeAddress = UserDetails.SkypeAddress ?? string.Empty,
                    StaffMember = UserDetails.StaffMember ?? string.Empty,
                    StateID = UserDetails.StateID ?? string.Empty,
                    State_pKey = UserDetails.State_pKey ?? string.Empty,
                    TimezonePKey = UserDetails.TimezonePKey ?? string.Empty,
                    Title = UserDetails.Title ?? string.Empty,
                    URL = UserDetails.URL ?? string.Empty,
                    VIP = UserDetails.VIP ?? string.Empty,
                    ZipCode = UserDetails.ZipCode ?? string.Empty,
                    PhoneTypeID = UserDetails.PhoneTypeID ?? string.Empty,
                    PhoneTypeID2 = UserDetails.PhoneTypeID2 ?? string.Empty,
                    Member = UserDetails.Member ?? string.Empty,
                    LinkedInProfile = UserDetails.LinkedInProfile ?? string.Empty,
                    showJournal = UserDetails.showJournal ?? string.Empty,
                    TimeZone = UserDetails.TimeZone ?? string.Empty,
                    strEmailUsed = UserDetails.strEmailUsed ?? string.Empty,
                    UL = UserDetails.UL ?? string.Empty,
                    ParentOrganization_pKey = UserDetails.ParentOrganization_pKey ?? string.Empty,
                    Salutation_pKey = SalutationModel1 != null ? SalutationModel1.pKey : string.Empty,
                    Suffix_pkey = SuffixModel1 != null ? SuffixModel1.strText : string.Empty,
                    Timezone = UserDetails.Timezone ?? string.Empty,
                    Suffix = SuffixModel1 != null ? SuffixModel1.strText : string.Empty,
                    GetJournal = UserDetails.GetJournal ?? string.Empty,
                    EmailUsed = UserDetails.EmailUsed ?? string.Empty
                };
                var result = await webAPiCallServices.UpdateUserList(uList);
                if (result == "Updated")
                {
                    await Application.Current.MainPage.DisplayAlert("Profile updated", " ", "Ok");
                   // await Application.Current.MainPage.Navigation.PopAsync();
                    await Application.Current.MainPage.Navigation.PushAsync(new Views.ProfilePages.ProfileViewPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error in updating profile",result, "Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Helper.HideLoader();

            }
            
            Helper.HideLoader();
        }

        private bool _Emailtousevisible;

        public bool Emailtousevisible
        {
            get { return _Emailtousevisible; }
            set 
            { 
                _Emailtousevisible = value;
                OnPropertyChanged("Emailtousevisible");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
