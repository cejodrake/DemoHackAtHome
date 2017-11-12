using Android.App;
using Android.OS;
using HackAtHome.CustomAdapters;

namespace HackAtHomePresentation.Fragmer
{
    class EvidenciaFragmet : Fragment
    {
        public EvidencesAdapter Adapter { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }
    }
}