using System;
using System.IO;
using MAGIApp.Services;
using MAGIApp.Utils;
using MAGIApp.ViewModel.ProfileViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static Xamarin.Essentials.AppleSignInAuthenticator;

namespace MAGIApp.Views.ProfilePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePageViewModel editProfilePageViewModel;
        public WebAPiCallServices webAPiCallServices;
        public string _file_choosen;
        
        public EditProfilePage()
        {
            InitializeComponent();
            BindingContext = editProfilePageViewModel = new EditProfilePageViewModel();
            webAPiCallServices = new WebAPiCallServices();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            editProfilePageViewModel.DepartmentLabel = UserDetails.Department;

            editProfilePageViewModel.FirstName = UserDetails.Firstname;
            editProfilePageViewModel.MiddleName = UserDetails.MiddleName;
            editProfilePageViewModel.LastName = UserDetails.Lastname;
            editProfilePageViewModel.UserName = Util.DisplayName;
            editProfilePageViewModel.ZipcodeLabel = UserDetails.ZipCode;
            editProfilePageViewModel.UserMailidLabel = UserDetails.Email;
            editProfilePageViewModel.UserMailidLabel2 = UserDetails.Email2;
            if (string.IsNullOrEmpty(editProfilePageViewModel.UserMailidLabel2))
            {
                editProfilePageViewModel.Emailtousevisible = false;
            }
            else
            {
                editProfilePageViewModel.Emailtousevisible = true;
            }
            editProfilePageViewModel.AccountLabel = UserDetails.Account_pkey;
            editProfilePageViewModel.JobTittleLabel = UserDetails.Title;
            editProfilePageViewModel.OrganizationLabel = UserDetails.OrganizationID;
            editProfilePageViewModel.PhoneLabel = UserDetails.Phone;
            editProfilePageViewModel.PhoneLabel2 = UserDetails.Phone2;
            if (!string.IsNullOrEmpty(UserDetails.StateID))
            {
                editProfilePageViewModel.StateLabel = UserDetails.StateID;
            }
            else
            {
                editProfilePageViewModel.StateLabel = UserDetails.OtherState;
            }
            editProfilePageViewModel.CityLabel = UserDetails.City;
            editProfilePageViewModel.TimeZoneLabel = UserDetails.Timezone;
            editProfilePageViewModel.CountryLabel = UserDetails.CountryID;

            editProfilePageViewModel.SalutationLabel = UserDetails.SalutationID;
            editProfilePageViewModel.SuffixLabel = UserDetails.Suffix_pkey;
            editProfilePageViewModel.LicenseTypeLabel = UserDetails.LicenseTypeID;
            editProfilePageViewModel.LicenseStateLabel = UserDetails.LicenseState;
            editProfilePageViewModel.LicenseNumberLabel = UserDetails.LicenseNumber;

            editProfilePageViewModel.Address1Label = UserDetails.Address1;
            editProfilePageViewModel.Address2Label = UserDetails.Address2;
            editProfilePageViewModel.PersonalBio = UserDetails.PersonalBio;
            editProfilePageViewModel.AboutMe = UserDetails.AboutMe;
            editProfilePageViewModel.PhoneTypeID = UserDetails.PhoneTypeID;
            editProfilePageViewModel.PhoneTypeID2 = UserDetails.PhoneTypeID2;

            editProfilePageViewModel.NickNameLabel = UserDetails.Nickname;
            editProfilePageViewModel.PhoneticNameLabel = UserDetails.PhoneticName;
            editProfilePageViewModel.SignInNameLabel = UserDetails.UL;

            editProfilePageViewModel.ExtLabel = UserDetails.Phone1Ext;
            editProfilePageViewModel.CcLabel = UserDetails.Phone1CC;
            editProfilePageViewModel.ExtLabel2 = UserDetails.Phone2Ext;
            editProfilePageViewModel.CcLabel2 = UserDetails.Phone2CC;

            editProfilePageViewModel.DegreeLabel = UserDetails.Degrees;
            editProfilePageViewModel.LinkedInLabel = UserDetails.LinkedInProfile;
            editProfilePageViewModel.SkypeLabel = UserDetails.SkypeAddress;
            if (UserDetails.Member == "True")
            {
                editProfilePageViewModel.MemberLabel = "True";

            }
            else
            {
                editProfilePageViewModel.MemberLabel = "False";
            }
            if (UserDetails.showJournal == "Yes")
            {
                editProfilePageViewModel.JournalLabel = "True";

            }
            else
            {
                editProfilePageViewModel.JournalLabel = "False";
            }
            if (UserDetails.strEmailUsed == "First")
            {
                FirstEmailRB.IsChecked = true;
                SecondEmailRB.IsChecked = false;
                BothEmailRB.IsChecked = false;
            }
            if (UserDetails.strEmailUsed == "Second")
            {
                FirstEmailRB.IsChecked = false;
                SecondEmailRB.IsChecked = true;
                BothEmailRB.IsChecked = false;
            }
            else if (UserDetails.strEmailUsed == "Both")
            {
                FirstEmailRB.IsChecked = false;
                SecondEmailRB.IsChecked = false;
                BothEmailRB.IsChecked = true;
            }
            if (UserDetails.AllowCall == "True")
            {
            }
            else
            {
            }


        }

        private void MemberCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
                editProfilePageViewModel.MemberLabel = "True";
            }
            else
            {
                editProfilePageViewModel.MemberLabel = "False";
            }
        }

        private void JournalCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
                editProfilePageViewModel.JournalLabel = "True";
            }
            else
            {
                editProfilePageViewModel.JournalLabel = "False";
            }
        }

        private void FirstEmailRB_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                editProfilePageViewModel.EmailLabel = "0";
            }

        }

        private void SecondEmailRB_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                editProfilePageViewModel.EmailLabel = "1";
            }

        }

        private void BothEmailRB_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                editProfilePageViewModel.EmailLabel = "2";
            }
        }

        private void YesCallRB_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                editProfilePageViewModel.AllowCallLabel = "True";
            }
        }

        private void NoCallRB_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                editProfilePageViewModel.AllowCallLabel = "False";
            }
        }

        private double width = 0;
        private double height = 0;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    editProfilePageViewModel.Heightpara = 45; //landscape
                }
                else
                {
                    editProfilePageViewModel.Heightpara = GridLength.Auto; //potrait
                }
            }
        }
        private void Email2Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (!string.IsNullOrEmpty(entry.Text))
            {
                editProfilePageViewModel.Emailtousevisible = true;
            }
            else
            {
                editProfilePageViewModel.Emailtousevisible = false;
            }
        }

        private void PersonalBioChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            int MaxLimit = 500;
            BioCounterText.IsVisible = true;
            if (newText.Length > MaxLimit)
                BioCounterText.Text = MaxLimit + " of " + MaxLimit + " Characters Used";
            else
                BioCounterText.Text = newText.Length + " of " + MaxLimit + " Characters Used";
        }

        private void UserRoleChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            int MaxLimit = 500;
            UserRoleText.IsVisible = true;
            if (newText.Length > MaxLimit)
                UserRoleText.Text = MaxLimit + " of " + MaxLimit + " Characters Used";
            else
                UserRoleText.Text = newText.Length + " of " + MaxLimit + " Characters Used";
        }

        private async void Handle_Tapped(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Select action", "Cancel", null, "Take new picture", "Upload a picture");
            if (action.Equals("Take new picture"))
            {
                   await CrossMedia.Current.Initialize();
                    if(!CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Error", "not Allowed", "cancel");
                }
                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Images",
                        DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                        SaveToAlbum = true,
                        Name = "elimar-profile.jpg"
                    });
                
                editProfilePageViewModel.UserImg = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
                
            }
            else if (action.Equals("Upload a picture"))
            {
                
                
                
                    var file = await CrossMedia.Current.PickPhotoAsync(null);
                    editProfilePageViewModel.UserImg = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });
                
                uploadPicture();
            }           
        }

        private async void takePicture()
        {
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Images",
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,                
                SaveToAlbum = true,
                Name = "elimar-profile.jpg"
            });

            editProfilePageViewModel.UserImg = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void uploadPicture()
        {
            var file = await CrossMedia.Current.PickPhotoAsync(null);

            

            editProfilePageViewModel.UserImg = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            
        }

        async Task<FileResult> choose_file(System.Object sender, System.EventArgs e)
        {
            string Text;
            try
            {
                var result = await FilePicker.PickAsync(null);
                if (result != null)
                {
                    Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("pdf", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("docx", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("text", StringComparison.OrdinalIgnoreCase)
                        )
                    {
                        editProfilePageViewModel.file_choosen = result.FileName;
                        return result;
                    }
                }                
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }
    }
}