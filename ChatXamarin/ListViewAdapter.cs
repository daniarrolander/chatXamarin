using Android.Widget;
using System;
using Android.Views;
using Java.Lang;
using System.Collections.Generic;
using Android.Content;

namespace ChatXamarin
{
    internal class ListViewAdapter : BaseAdapter
    {
        private List<MessageContent> lsMessage;
        private MainActivity mainActivity;

        public ListViewAdapter(MainActivity mainActivity, List<MessageContent> lsMessage)
        {
            this.mainActivity = mainActivity;
            this.lsMessage = lsMessage;
        }

        public override int Count => lsMessage.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mainActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
            View itemView = inflater.Inflate(Resource.Layout.list_item, null);
            TextView message_user, message_content, message_time;
            message_user = itemView.FindViewById<TextView>(Resource.Id.message_user);
            message_content = itemView.FindViewById<TextView>(Resource.Id.message_text);
            message_time = itemView.FindViewById<TextView>(Resource.Id.message_time);

            message_user.Text = lsMessage[position].Email;
            message_time.Text = lsMessage[position].Time;
            message_content.Text = lsMessage[position].Message;

            return itemView;
        }
    }
}