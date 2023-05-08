using AntroStop.BlazorUI.Components;
using AntroStop.BlazorUI.Infrastructure.Extensions;
using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast;
using Blazored.Toast.Services;
using Darnton.Blazor.DeviceInterop.Geolocation;
using Darnton.Blazor.Leaflet.LeafletMap;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntroStop.BlazorUI.Pages
{
    public partial class EditViolationPage
    {
        [Parameter]
        public string ID { get; set; }

        public List<string> filesUrl { get; set; }

        //Создание заявки
        private ViolationsInfo violationsInfo = new ViolationsInfo();
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IWebViolationsRepository<ViolationsInfo> violationsRepository { get; set; }
        [Inject]
        public IWebElementsRepository<ElementsInfo> elementsRepository { get; set; }

        //Всплывающие окна
        [Inject]
        public IToastService toast { get; set; }
        private ToastParameters _toastParameters = default!;
        //Карта
        [Inject] public IGeolocationService GeolocationService { get; set; }
        protected Map PositionMap;
        protected TileLayer PositionTileLayer;
        protected Marker CurrentPositionMarker;
        protected GeolocationResult CurrentPositionResult { get; set; }
        protected string CurrentLatitude => CurrentPositionResult?.Position?.Coords?.Latitude.ToString("F2");
        protected string CurrentLongitude => CurrentPositionResult?.Position?.Coords?.Longitude.ToString("F2");
        protected bool ShowCurrentPositionError => CurrentPositionResult?.Error != null;

        public EditViolationPage() : base()
        {
            PositionMap = new Map("geolocationPointMap", new MapOptions //Russian Federation
            {
                Center = new LatLng(60.965795, 94.772974),
                Zoom = 2
            });

            PositionTileLayer = new TileLayer(
                "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
                new TileLayerOptions
                {
                    Attribution = @"Map data &copy; <a href=""https://www.openstreetmap.org/"">OpenStreetMap</a> contributors, " +
                        @"<a href=""https://creativecommons.org/licenses/by-sa/2.0/"">CC-BY-SA</a>"
                }
            );
        }

        private async Task Save()
        {
            await violationsRepository.Update(violationsInfo);


            _toastParameters = new ToastParameters();
            _toastParameters.Add(nameof(MyToastComponent.Title), "Успех!");
            _toastParameters.Add(nameof(MyToastComponent.ToastParam), "Заявка изменена успешно!");
            _toastParameters.Add(nameof(MyToastComponent.Type), "success");

            toast.ShowToast<MyToastComponent>(_toastParameters);



            Navigation.NavigateTo("/Violations");
        }

        private async void LoadFiles()
        {
            var files = await elementsRepository.GetAllByID(violationsInfo.Id.ToString());

            if (files != null)
            {
                foreach (var file in files)
                {
                    filesUrl.Add(file.FileName);
                }
            }
            StateHasChanged();
        }
        private async void ShowCurrentPosition()
        {
            string[] vals = violationsInfo.Coordinates.Split(";");
            LatLng latLng = new LatLng(double.Parse(vals[0]), double.Parse(vals[1]));

            CurrentPositionMarker = new Marker(latLng, null);
            await CurrentPositionMarker.AddTo(PositionMap);
            LoadFiles();
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            filesUrl = new List<string>();
            violationsInfo = await violationsRepository.Get(ID);
            
            ShowCurrentPosition();
        }
    }
}
