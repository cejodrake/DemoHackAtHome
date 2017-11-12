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
using HackAtHome.Entities;
using System.Threading.Tasks;
using HackAtHome.CustomAdapters;
using HackAtHomePresentation.Fragmer;

namespace HackAtHomePresentation
{
    [Activity(Label = "@string/ApplicationName" , Icon = "@drawable/icon")]
    public class EvidenciasDetalleActivity : Activity
    {
        string token = string.Empty;
        ListView listEvidencias;
        string Nombre = string.Empty;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Evidencias);

            var fullName = FindViewById<TextView>(Resource.Id.textViewNombre);
             token = Intent.GetStringExtra("Token");
            fullName.Text = Intent.GetStringExtra("Nombre");
            listEvidencias = FindViewById<ListView>(Resource.Id.listViewEvidencias);
            listEvidencias.ItemClick += ListEvidencias_ItemClick;

            var datos = this.FragmentManager.FindFragmentByTag("Data") as EvidenciaFragmet;

            if (datos == null)
            {
            
                EvidenciasListas(token);
            }
            else
            {
                listEvidencias.Adapter = datos.Adapter;
            }
           




        }

        private void ListEvidencias_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var service = new HackAtHome.SAL.ServiceClient();
            var evidence = (listEvidencias.Adapter as EvidencesAdapter)[e.Position];

            var intenDetalleItem = new Android.Content.Intent(this, typeof(EvidenciaItemDetalle));
            intenDetalleItem.PutExtra("Title", evidence.Title);
            intenDetalleItem.PutExtra("Status", evidence.Status);
            intenDetalleItem.PutExtra("EvidenceID", evidence.EvidenceID);
            intenDetalleItem.PutExtra("FullName", Nombre);
            intenDetalleItem.PutExtra("Token", token);
            StartActivity(intenDetalleItem);
        }

        private async void EvidenciasListas(string token)

        {
            var service = new HackAtHome.SAL.ServiceClient();

            var e = await service.GetEvidencesAsync(token);
           
            var adapter 
             = new HackAtHome.CustomAdapters.EvidencesAdapter
                (this, e,Resource.Layout.ListEvidencias ,Resource.Id.textViewLaboratorio, Resource.Id.textViewAprobado);


            listEvidencias.Adapter = adapter;

            var data = new EvidenciaFragmet
            {
                Adapter = adapter
            };
            var FragmentTransaction = this.FragmentManager.BeginTransaction();
            FragmentTransaction.Add(data, "Data");
            FragmentTransaction.Commit();


        }
    }
}