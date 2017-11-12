using Android.App;
using Android.Widget;
using Android.OS;
using System;
using HackAtHome.Entities;
using Android.Content;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HackAtHomePresentation
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            var buttonValidar = FindViewById<Button>(Resource.Id.buttonValidar);

            buttonValidar.Click += ButtonValidar_Click;   

        }

        private void ButtonValidar_Click(object sender, System.EventArgs e)
        {

                AutencicationUsuario();

        }

        private async void AutencicationUsuario()
        {
            var correo = FindViewById<EditText>(Resource.Id.editTextCorreo);
            var password = FindViewById<EditText>(Resource.Id.editTextPassword);
            var service = new HackAtHome.SAL.ServiceClient();

            ResultInfo resultAutentication = await service.AutenticateAsync(correo.Text, password.Text);

            if (resultAutentication.Token != null)
            {
                var intentEvidencias = new Intent(this, typeof(EvidenciasDetalleActivity));
                intentEvidencias.PutExtra("Nombre", resultAutentication.FullName);
                intentEvidencias.PutExtra("Token", resultAutentication.Token);

                StartActivity(intentEvidencias);

                


            }
        }
    }
}

