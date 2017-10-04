using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Android.Support.V7.App;
using Android.Gms.Tasks;

namespace ChatXamarin
{
    [Activity(Label = "SignInView", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class SignInView : AppCompatActivity, IOnCompleteListener
    {
        private FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SignIn);
            auth = FirebaseAuth.Instance;

            var edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            var edtPassword = FindViewById<EditText>(Resource.Id.edtPassword);
            var btnSubmit = FindViewById<Button>(Resource.Id.btnSign);
            btnSubmit.Click += delegate
            {
                auth.SignInWithEmailAndPassword(edtEmail.Text, edtPassword.Text)
                .AddOnCompleteListener(this);
            };
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Toast.MakeText(this, "SignIn Succesfull!", ToastLength.Short).Show();
                StartActivity(typeof(MainActivity));
            }
            else
            {
                Toast.MakeText(this, "SignIn Failed", ToastLength.Short).Show();
                Finish();
            }
        }
    }
}