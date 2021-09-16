using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;
using SampleClientSide.Helpers;
using System.Threading.Tasks;

namespace SampleClientSide.Pages
{
    public partial class Home : BindableBase
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

        public async Task RequestDevice()
        {
            Device = null;

            var q = new RequestDeviceQuery
            {
                AcceptAllDevices = DeviceFilter.AllowAllDevices,
            };

            if (!DeviceFilter.AllowAllDevices)
            {
                q.Filters.Add(new Filter
                {
                    Name = string.IsNullOrWhiteSpace(DeviceFilter.DeviceName)
                        ? null
                        : DeviceFilter.DeviceName,
                    NamePrefix = string.IsNullOrWhiteSpace(DeviceFilter.DeviceNamePrefix)
                        ? null
                        : DeviceFilter.DeviceNamePrefix,
                });
            }

            if (!string.IsNullOrEmpty(DeviceFilter.ServiceUuid))
            {
                q.OptionalServices.Add(DeviceFilter.ServiceUuid);
            }

            Device = await BluetoothNavigator.RequestDevice(q);
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
