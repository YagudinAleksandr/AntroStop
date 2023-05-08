using System.Collections.Generic;
using System.Threading.Tasks;
using AntroStop.Domain.Base.Models;
using AntroStop.Domain.Base.Models.Users;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast.Services;
using Blazored.Toast;
using Darnton.Blazor.DeviceInterop.Geolocation;
using Microsoft.AspNetCore.Components;
using Darnton.Blazor.Leaflet.LeafletMap;
using AntroStop.BlazorUI.Infrastructure.Extensions;
using AntroStop.BlazorUI.Components;

namespace AntroStop.BlazorUI.Pages
{
    public partial class Index
    {
        private int usersCount;
        private int closedViolations;
        private int inboxViolations;
        [Inject]
        private IWebUsersRepository<UsersInfo> Users { get; set; }
        [Inject]
        private IWebViolationsRepository<ViolationsInfo> ViolationsRepository { get; set; }


        protected override async Task OnInitializedAsync()
        {
            usersCount = await Users.Count();
            closedViolations = await ViolationsRepository.GetCountByStatus("Закрыта");
            inboxViolations = await ViolationsRepository.GetCountByStatus("Принята");
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            violationsInfo = new ViolationsInfo();
        }

        //Создание заявки
        private ViolationsInfo violationsInfo;
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IWebViolationsRepository<ViolationsInfo> violationsRepository { get; set; }

        private string UserID = string.Empty;

        //Файлы
        public List<ElementsInfo> Elements = new List<ElementsInfo>();
        [Inject]
        public IWebElementsRepository<ElementsInfo> ElementsRepository { get; set; }

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


        public Index() : base()
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

            var violation = await violationsRepository.Add(violationsInfo);

            if (Elements.Count > 0)
            {
                foreach (var element in Elements)
                {
                    element.ViolationID = violation.Id;
                    element.Type = "File";
                    await ElementsRepository.Add(element);
                }
            }

            _toastParameters = new ToastParameters();
            _toastParameters.Add(nameof(MyToastComponent.Title), "Успех!");
            _toastParameters.Add(nameof(MyToastComponent.ToastParam), "Заявка подана успешно!");
            _toastParameters.Add(nameof(MyToastComponent.Type), "success");

            toast.ShowToast<MyToastComponent>(_toastParameters);



            Navigation.NavigateTo($"/ViolationsByUser/{violationsInfo.UserID}");
        }
        private void AssignImageUrl(string imgUrl)
        {
            Elements.Add(new ElementsInfo { FileName = imgUrl });
        }
    }


}
