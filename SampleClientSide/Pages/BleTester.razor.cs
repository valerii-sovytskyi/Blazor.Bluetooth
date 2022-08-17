using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;
using SampleClientSide.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SampleClientSide.Pages
{
    public partial class BleTester : BindableBase
    {
        [Inject]
        public IBluetoothNavigator BluetoothNavigator { get; set; }

        public DeviceFilter DeviceFilter { get; set; } = new DeviceFilter();

        private IDevice _device;
        public IDevice Device
        {
            get => _device;
            set => SetProperty(ref _device, value);
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
                Device = null;

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

                Device = await BluetoothNavigator.RequestDevice(query);
            }
            catch (AdvertisementsUnavailableException ex)
            {
                AdvertisementsFeatureDissabled = true;
                Logs.Add($"AdvertisementsUnavailableException: {ex.Message}");
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
            Device.OnAdvertisementReceived -= Device_OnAdvertisementReceived;
        }

        public async Task ConnectDevice()
        {
            IsBusy = true;
            AdvertisementsReceiveActivated = false;
            BluetoothAdvertisingEvent = null;

            try
            {
                await Device.Gatt.Connect();
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
                await Device.Gatt.Disonnect();
                StateHasChanged();
            }
            catch (System.Exception ex)
            {
                Logs.Add($"Exception: {ex.Message}");
            }

            if (!await Device.Gatt.GetConnected())
            {
                Device = null;
            }

            IsBusy = false;
        }

        public async Task UpdateIsConnected()
        {
            IsBusy = true;

            try
            {
                await Device.Gatt.GetConnected();
                StateHasChanged();
            }
            catch (System.Exception ex)
            {
                Logs.Add($"Exception: {ex.Message}");
            }

            IsBusy = false;
        }

        private async Task StartReceivingAdvertisements()
        {
            Device.OnAdvertisementReceived += Device_OnAdvertisementReceived;
            await Device.WatchAdvertisements();
            AdvertisementsReceiveActivated = true;
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
