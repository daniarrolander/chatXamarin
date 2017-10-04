using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase.Xamarin.Database;
using System.Collections.Generic;
using Android.Support.Design.Widget;
using Firebase.Database;
using System;
using Firebase.Auth;
using Android.Content;

namespace ChatXamarin
{
    [Activity(Label = "ChatXamarin", MainLauncher = false, Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity, IValueEventListener
    {
        private FirebaseClient firebase;
        private List<MessageContent> lsMessage = new List<MessageContent>();
        private ListView lstChat;
        private EditText edtChat;
        private FloatingActionButton fab;

        public int MyResultCode = 1;

        public override void OnActivityReenter(int resultCode, Intent data)
        {
            base.OnActivityReenter(resultCode, data);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));
            FirebaseDatabase.Instance.GetReference("chats").AddValueEventListener(this);
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            edtChat = FindViewById<EditText>(Resource.Id.input);
            lstChat = FindViewById<ListView>(Resource.Id.list_of_message);
            fab.Click += delegate { PostMessage(); };

            if (FirebaseAuth.Instance.CurrentUser == null)
            {
                StartActivityForResult(new Android.Content.Intent(this, typeof(SignInView)), MyResultCode);
            }
            else
            {
                Toast.MakeText(this, "Welcome" + FirebaseAuth.Instance.CurrentUser.Email, ToastLength.Short).Show();
                DisplayChatMessage();
            }
        }

        private async void PostMessage()
        {
            var items = await firebase.Child("chats").PostAsync(new MessageContent(FirebaseAuth.Instance.CurrentUser.Email, edtChat.Text));
            edtChat.Text = "";
        }

        public void OnCancelled(DatabaseError error)
        {
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            DisplayChatMessage();
        }

        private async void DisplayChatMessage()
        {
            lsMessage.Clear();
            var items = await firebase.Child("chats")
                .OnceAsync<MessageContent>();
            foreach (var item in items)
                lsMessage.Add(item.Object);
            ListViewAdapter adapter = new ListViewAdapter(this, lsMessage);
            lstChat.Adapter = adapter;
        }
    }
}