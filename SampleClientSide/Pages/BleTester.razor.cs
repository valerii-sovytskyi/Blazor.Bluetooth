using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;
using SampleClientSide.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SampleClientSide.Pages
{
    public partial class BleTester : BindableBase
    {
        [Inject]
        public IBluetoothNavigator BluetoothNavigator { get; set; }

        public List<IDevice> RequestedDevices { get; set; } = new List<IDevice>();

        public List<IDevice> GotDevices { get; set; } = new List<IDevice>();

        public DeviceFilter DeviceFilter { get; set; } = new DeviceFilter();

        private IDevice _currentdevice;
        public IDevice CurrentDevice
        {
            get => _currentdevice;
            set => SetProperty(ref _currentdevice, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private bool _advertisementsFeatureDissabled;
        public bool AdvertisementsFeatureDissabled
        {
            get => _advertisementsFeatureDissabled;
            set => SetProperty(ref _advertisementsFeatureDissabled, value);
        }

        private IBluetoothAdvertisingEvent _bluetoothAdvertisingEvent;
        public IBluetoothAdvertisingEvent BluetoothAdvertisingEvent
        {
            get => _bluetoothAdvertisingEvent;
            set => SetProperty(ref _bluetoothAdvertisingEvent, value);
        }

        private bool _advertisementsReceiveActivated;
        public bool AdvertisementsReceiveActivated
        {
            get => _advertisementsReceiveActivated;
            set => SetProperty(ref _advertisementsReceiveActivated, value);
        }

        public ObservableCollection<string> Logs { get; set; } = new ObservableCollection<string>
        {
            "Logs:"
        };

        public async Task RequestDevice()
        {
            IsBusy = true;
            AdvertisementsReceiveActivated = false;
            BluetoothAdvertisingEvent = null;

            try
            {
                var isBleAvailable = await BluetoothNavigator.GetAvailability();
                if (!isBleAvailable)
                {
                    Logs.Add($"The BLE is not available on your browser");
                }

                CurrentDevice = null;

                var query = new RequestDeviceQuery { AcceptAllDevices = DeviceFilter.AllowAllDevices };

                if (!DeviceFilter.AllowAllDevices)
                {
                    query.Filters = new List<Filter>
                    {
                        new Filter
                        {
                            Name = string.IsNullOrWhiteSpace(DeviceFilter.DeviceName)
                                ? null
                                : DeviceFilter.DeviceName,
                            NamePrefix = string.IsNullOrWhiteSpace(DeviceFilter.DeviceNamePrefix)
                                ? null
                                : DeviceFilter.DeviceNamePrefix,
                        }
                    };
                }

                if (!string.IsNullOrEmpty(DeviceFilter.ServiceUuid))
                {
                    query.OptionalServices.Add(DeviceFilter.ServiceUuid);
                }

                CurrentDevice = await BluetoothNavigator.RequestDevice(query);

                if (RequestedDevices.Any(x => x.Id != CurrentDevice.Id))
                {
                    RequestedDevices.Add(CurrentDevice);
                }
            }
            catch (System.Exception ex)
            {
                Logs.Add($"Exception: {ex.Message}");
            }

            IsBusy = false;
        }

        private void Device_OnAdvertisementReceived(IBluetoothAdvertisingEvent bluetoothAdvertisingEvent)
        {
            BluetoothAdvertisingEvent = bluetoothAdvertisingEvent;
            CurrentDevice.OnAdvertisementReceived -= Device_OnAdvertisementReceived;
        }

        public async Task ConnectDevice(IDevice device)
        {
            IsBusy = true;
            AdvertisementsReceiveActivated = false;
            BluetoothAdvertisingEvent = null;

            try
            {
                await device.Gatt.Connect();

                CurrentDevice = device;
                StateHasChanged();
            }
            catch (System.Exception ex)
            {
                Logs.Add($"Exception: {ex.Message}");
            }

            IsBusy = false;
        }

        public async Task DisconnectDevice()
        {
            IsBusy = true;
            AdvertisementsReceiveActivated = false;
            BluetoothAdvertisingEvent = null;

            try
            {
                await CurrentDevice.Gatt.Disonnect();
                StateHasChanged();
            }
            catch (System.Exception ex)
            {
                Logs.Add($"Exception: {ex.Message}");
            }

            if (!await CurrentDevice.Gatt.GetConnected())
            {
                CurrentDevice = null;
            }

            IsBusy = false;
        }

        public async Task UpdateIsConnected()
        {
            IsBusy = true;

            try
            {
                await CurrentDevice.Gatt.GetConnected();
                StateHasChanged();
            }
            catch (System.Exception ex)
            {
                Logs.Add($"Exception: {ex.Message}");
            }

            IsBusy = false;
        }

        public async Task OnGetDeviceClicked()
        {
            try
            {
                GotDevices.Clear();
                var devices = await BluetoothNavigator.GetDevices();
                if (devices != null)
                {
                    GotDevices.AddRange(devices);
                }
            }
            catch (System.Exception ex)
            {
                Logs.Add($"Exception: {ex.Message}");
            }
        }

        public bool? IsAvailable { get; set; }

        public async Task OnGetAvailabilityClicked()
        {
            try
            {
                IsAvailable = null;
                Logs.Add($"Subscribing to OnAvailabilityChanged event");
                BluetoothNavigator.OnAvailabilityChanged -= BluetoothNavigator_OnAvailabilityChanged;
                BluetoothNavigator.OnAvailabilityChanged += BluetoothNavigator_OnAvailabilityChanged;
                IsAvailable = await BluetoothNavigator.GetAvailability();
            }
            catch (System.Exception ex)
            {
                Logs.Add($"Exception: {ex.Message}");
            }
        }

        private void BluetoothNavigator_OnAvailabilityChanged()
        {
            Logs.Add($"BluetoothNavigator_OnAvailabilityChanged called");
        }

        private async Task StartReceivingAdvertisements()
        {
            try
            {
                CurrentDevice.OnAdvertisementReceived += Device_OnAdvertisementReceived;
                await CurrentDevice.WatchAdvertisements();
                AdvertisementsReceiveActivated = true;
            }
            catch (AdvertisementsUnavailableException ex)
            {
                AdvertisementsFeatureDissabled = true;
                Logs.Add($"AdvertisementsUnavailableException: {ex.Message}");
            }
        }
    }

    public class DeviceFilter : BindableBase
    {
        private string _deviceName;
        public string DeviceName
        {
            get => _deviceName;
            set => SetProperty(ref _deviceName, value);
        }

        private string _deviceNamePrefix;
        public string DeviceNamePrefix
        {
            get => _deviceNamePrefix;
            set => SetProperty(ref _deviceNamePrefix, value);
        }

        private bool _allowAllDevices = true;
        public bool AllowAllDevices
        {
            get => _allowAllDevices;
            set => SetProperty(ref _allowAllDevices, value);
        }

        private string _serviceUuid;
        public string ServiceUuid
        {
            get => _serviceUuid;
            set => SetProperty(ref _serviceUuid, value);
        }
    }
}
