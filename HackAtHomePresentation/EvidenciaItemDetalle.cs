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
using Android.Webkit;
using HackAtHome.Entities;
using HackAtHome.SAL;

namespace HackAtHomePresentation
{
    [Activity(Label =  "@string/ApplicationName", Icon = "@drawable/icon")]
    public class EvidenciaItemDetalle : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetalleEvidencia);

             var nombreCompleto = FindViewById<TextView>(Resource.Id.TextViewItemNombre);

            nombreCompleto.Text = Intent.GetStringExtra("Nombre");

            var token = Intent.GetStringExtra("Token");
            var evidenciaId = Intent.GetIntExtra("EvidenceID", 0);
            var titulo = Intent.GetStringExtra("Title");
            var estado = Intent.GetStringExtra("Status");

            var service = new HackAtHome.SAL.ServiceClient();

            var evidencia = await service.GetEvidenceByIDAsync(token, evidenciaId);

            var tituloText = FindViewById<TextView>(Resource.Id.TextViewEvidenciaItemTitulo);
          
            tituloText.Text = titulo;

           var description = FindViewById<WebView>(Resource.Id.evidenceDescription);
            description.LoadDataWithBaseURL(null, evidencia.Description, "text/html", "utf-8", null);


            var estadoText = FindViewById<TextView>(Resource.Id.TextViewEvidenciaEstadoItem);
            estadoText.Text = estado;

            var imagen = FindViewById<ImageView>(Resource.Id.ImageViewEvidenciaItem);
            Koush.UrlImageViewHelper.SetUrlDrawable(imagen, evidencia.Url);

            var microsoftEvidence = new LabItem
            {
                Email = "j_calix2002@hotmail.com",
                Lab = "Hack@Home",
                DeviceId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId)
            };

            var microsoftClient = new MicrosoftServiceClient();
            await microsoftClient.SendEvidence(microsoftEvidence);






        }
    }
}