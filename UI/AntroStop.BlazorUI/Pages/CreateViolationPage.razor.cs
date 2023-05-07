using AntroStop.BlazorUI.Components;
using AntroStop.BlazorUI.Infrastructure.Extensions;
using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast;
using Blazored.Toast.Services;
using Darnton.Blazor.DeviceInterop.Geolocation;
using Darnton.Blazor.Leaflet.LeafletMap;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AntroStop.BlazorUI.Pages
{
    public partial class CreateViolationPage : ComponentBase
    {
        //Создание заявки
        private ViolationsInfo violationsInfo;
        [Inject]
        public NavigationManager Navigation { get; set; }
        
        [Inject]
        public IWebViolationsRepository<ViolationsInfo> violationsRepository { get; set; }

        private string UserID = string.Empty;

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

        public CreateViolationPage() : base()
        {
            PositionMap = new Map("geolocationPointMap", new MapOptions //Centred on New Zealand
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
        public async void ShowCurrentPosition()
        {
            if (CurrentPositionMarker != null)
            {
                await CurrentPositionMarker.Remove();
            }
            CurrentPositionResult = await GeolocationService.GetCurrentPosition();
            if (CurrentPositionResult.IsSuccess)
            {
                CurrentPositionMarker = new Marker(
                        CurrentPositionResult.Position.ToLeafletLatLng(), null
                    );
                await CurrentPositionMarker.AddTo(PositionMap);

                violationsInfo.Coordinates = $"{CurrentLatitude};{CurrentLongitude}";
            }
            StateHasChanged();
        }

        //Сохранение заявки
        private async Task Save()
        {

            

            violationsInfo.Status = "Принята";
            violationsInfo.UserID = UserID;

            await violationsRepository.Add(violationsInfo);

            _toastParameters = new ToastParameters();
            _toastParameters.Add(nameof(MyToastComponent.Title), "Успех!");
            _toastParameters.Add(nameof(MyToastComponent.ToastParam), "Заявка подана успешно!");
            _toastParameters.Add(nameof(MyToastComponent.Type), "success");

            toast.ShowToast<MyToastComponent>(_toastParameters);

           

            Navigation.NavigateTo("/Violations");
        }

        protected override void OnInitialized()
        {
            violationsInfo = new ViolationsInfo();

        }
    }
}
