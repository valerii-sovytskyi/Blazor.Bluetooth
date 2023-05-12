window.ble = {};

// Helpers

var PairedBluetoothDevices = [];

function getPairedBluetoothDeviceById(deviceId) {
    var device = PairedBluetoothDevices.filter(function (item) {
        return item.id == deviceId;
    });

    return device[0];
}

async function getCharacteristic(deviceId, serviceId, characteristicId) {
    var device = getPairedBluetoothDeviceById(deviceId);

    var service = await device.gatt.getPrimaryService(serviceId);
    var characteristic = await service.getCharacteristic(characteristicId);
    return characteristic;
}

// End Helpers


// Device

function convertBluetoothAdvertisingEvent(event) {
    return {
        "InternalAppearance": event.appearance,
        "InternalDevice": event.device,
        "InternalManufacturerData": event.manufacturer_data,
        "InternalName": event.name,
        "InternalRssi": event.rssi,
        "InternalServiceData": event.service_data,
        "InternalTxPower": event.tx_power,
        "InternalUuids": event.uuids,
    }
}

var AdvertisementReceivedHandler = [];

window.ble.setAdvertisementReceivedHandler = (advertisementReceivedHandler) => {

    AdvertisementReceivedHandler = advertisementReceivedHandler;
}

window.ble.watchAdvertisements = async (deviceId) => {
    var device = getPairedBluetoothDeviceById(deviceId);
    if (!device.watchingAdvertisements) {
        device.addEventListener('advertisementreceived', handleAdvertisementReceived);
        device.watchAdvertisements();
    }
}

async function handleAdvertisementReceived(event) {
    if (AdvertisementReceivedHandler != null) {
        var convertedEvent = convertBluetoothAdvertisingEvent(event);
        await AdvertisementReceivedHandler.invokeMethodAsync('HandleAdvertisementReceived', convertedEvent);
    }
}

// End Device


// Service

function convertPrimaryService(service, deviceId) {
    return {
        "InternalIsPrimary": service.isPrimary,
        "InternalUuid": service.uuid,
        "InternalDeviceUuid": deviceId,
    }
}

window.ble.getPrimaryService = async (serviceId, deviceId) => {
    var device = getPairedBluetoothDeviceById(deviceId);
    var primaryService = await device.gatt.getPrimaryService(serviceId);
    return convertPrimaryService(primaryService, deviceId)
}

window.ble.getPrimaryServices = async (serviceId, deviceId) => {
    var device = getPairedBluetoothDeviceById(deviceId);
    var primaryServices = await device.gatt.getPrimaryServices(serviceId);
    return primaryServices.map(x => convertPrimaryService(x, deviceId));
}

// End Service


// Characteristic

function convertCharacteristic(characteristic, deviceId, serviceId) {
    return {
        "Properties": {
            "InternalAuthenticatedSignedWrites": characteristic.authenticatedSignedWrites,
            "InternalBroadcast": characteristic.broadcast,
            "InternalIndicate": characteristic.indicate,
            "InternalNotify": characteristic.notify,
            "InternalRead": characteristic.read,
            "InternalReliableWrite": characteristic.reliableWrite,
            "InternalWritableAuxiliaries": characteristic.writableAuxiliaries,
            "InternalWrite": characteristic.write,
            "InternalWriteWithoutResponse": characteristic.writeWithoutResponse,
        },
        "InternalUuid": characteristic.uuid,
        "InternalValue": characteristic.value,
        "InternalDeviceUuid": deviceId,
        "InternalServiceUuid": serviceId,
    }
}

window.ble.getCharacteristic = async (serviceId, characteristicId, deviceId) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);
    return convertCharacteristic(characteristic, deviceId, serviceId)
}

window.ble.getCharacteristics = async (serviceId, characteristicId, deviceId) => {
    var device = getPairedBluetoothDeviceById(deviceId);
    var service = await device.gatt.getPrimaryService(serviceId);
    var characteristics = await service.getCharacteristics(characteristicId);
    return characteristics.map(x => convertCharacteristic(x, deviceId, serviceId));
}

// End Characteristic


// Descriptors

function convertDescriptor(descriptor, characteristicId, deviceId, serviceId) {
    return {
        "InternalUuid": descriptor.uuid,
        "InternalValue": descriptor.value,
        "InternalDeviceUuid": deviceId,
        "InternalCharacteristicUuid": characteristicId,
        "InternalServiceUuid": serviceId,
    }
}

window.ble.getDescriptor = async (descriptorId, serviceId, characteristicId, deviceId) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);
    var descriptor = await characteristic.getDescriptor(descriptorId);
    return convertDescriptor(descriptor, characteristicId, deviceId, serviceId);
}

window.ble.getDescriptors = async (descriptorId, serviceId, characteristicId, deviceId) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);
    var descriptors = await characteristic.getDescriptors(descriptorId);
    return descriptors.map(x => convertDescriptor(x, characteristicId, deviceId, serviceId));
}

// End Descriptors


// Characteristic Start/Stop notifications

window.ble.startNotification = async (deviceId, serviceId, characteristicId) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);
    await characteristic.startNotifications();
    characteristic.addEventListener('characteristicvaluechanged', handleCharacteristicValueChanged);
}

window.ble.stopNotification = async (deviceId, serviceId, characteristicId) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);
    await characteristic.stopNotifications();
    characteristic.removeEventListener('characteristicvaluechanged', handleCharacteristicValueChanged);
}

// End Characteristic Start/Stop notifications


// Characteristic value changed

var CharacteristicValueHandler = [];

window.ble.setCharacteristicValueChangedHandler = (characteristicValueHandler) => {

    CharacteristicValueHandler = characteristicValueHandler;
}

async function handleCharacteristicValueChanged(event) {

    var value = event.target.value;

    var uint8Array = new Uint8Array(value.buffer);

    var array = Array.from(uint8Array)
    console.log(JSON.stringify(array));
    await CharacteristicValueHandler.invokeMethodAsync('HandleCharacteristicValueChanged', event.target.service.uuid, event.target.uuid, array);
}

// End Characteristic value changed


// Characteristic read/write value

window.ble.characteristicWriteValue = async (deviceId, serviceId, characteristicId, value) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);
    var b = Uint8Array.from(value);
    await characteristic.writeValue(b);
}

window.ble.characteristicWriteValueWithoutResponse = async (deviceId, serviceId, characteristicId, value) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);
    var b = Uint8Array.from(value);
    await characteristic.writeValueWithoutResponse(b);
}

window.ble.characteristicWriteValueWithResponse = async (deviceId, serviceId, characteristicId, value) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);
    var b = Uint8Array.from(value);
    await characteristic.writeValueWithResponse(b);
}

window.ble.characteristicReadValue = async (deviceId, serviceId, characteristicId) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);

    var value = await characteristic.readValue();
    var uint8Array = new Uint8Array(value.buffer);
    var array = Array.from(uint8Array);
    return array;
}

// End Characteristic write value


// Descriptor read/write

window.ble.descriptorReadValue = async (deviceId, serviceId, characteristicId, descriptorId) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);
    var descriptor = await characteristic.getDescriptor(descriptorId);

    var value = await descriptor.readValue();
    var uint8Array = new Uint8Array(value.buffer);
    var array = Array.from(uint8Array);
    return array;
}

window.ble.descriptorWriteValue = async (deviceId, serviceId, characteristicId, descriptorId, value) => {

    var characteristic = await getCharacteristic(deviceId, serviceId, characteristicId);
    var descriptor = await characteristic.getDescriptor(descriptorId);

    var b = Uint8Array.from(value);
    await descriptor.writeValue(b);
}

// End Descriptor read/write


// Bluetooth

function convertDevice(device) {
    return {
        "InternalName": device.name,
        "InternalId": device.id,
        "InternalGatt": {
            "InternalDeviceUuid": device.id,
            "InternalConnected": device.gatt.connected
        }
    };
}

window.ble.currentDevice = {}

window.ble.getCurrentDevice = () => {
    var device = window.ble.currentDevice;
    if (device !== null) {
        return convertDevice(device);
    }
    else {
        return null;
    }
}

window.ble.connectCurrentDevice = async () => {
    var device = window.ble.currentDevice;
    await device.gatt.connect();
}

window.ble.connectDevice = async (deviceId) => {
    var device = getPairedBluetoothDeviceById(deviceId);

    if (device !== null) {
        await device.gatt.connect();
        return convertDevice(device);
    }
    else {
        return null;
    }
}

window.ble.disconnectCurrentDevice = async () => {
    var device = window.ble.currentDevice;
    await device.gatt.disconnect();
}

window.ble.disconnectDevice = async (deviceId) => {
    var device = getPairedBluetoothDeviceById(deviceId);

    if (device !== null) {
        await device.gatt.disconnect();
        return convertDevice(device);
    }
    else {
        return null;
    }
}

window.ble.referringDevice = () => {

    var device = navigator.bluetooth.referringDevice;
    if (device === undefined) {
        throw 'Referring device is not supporting';
    }
    else {
        return convertDevice(device);
    }
}

window.ble.requestDevice = async (query) => {
    var objquery = JSON.parse(query);
    var device = await navigator.bluetooth.requestDevice(objquery);

    var alreadyPariedDevice = getPairedBluetoothDeviceById(device.id);
    if (alreadyPariedDevice != null) {
        var indexToRemove = PairedBluetoothDevices.findIndex(x => x.id == device.id);
        PairedBluetoothDevices.splice(indexToRemove, 1);
    }

    PairedBluetoothDevices.push(device);
    window.ble.currentDevice = device;

    if (device !== null) {
        console.log('> Bluetooth Device selected.');
    }

    return await window.ble.getCurrentDevice();
}

window.ble.getAvailability = async () => {
    return await navigator.bluetooth.getAvailability();
}

window.ble.getDevices = async () => {
    if (navigator.bluetooth.getDevices == undefined) {
        throw 'Get devices is not supporting';
    } else {
        var devices = await navigator.bluetooth.getDevices();

        devices.forEach((device) => {
            var alreadyPariedDevice = getPairedBluetoothDeviceById(device.id);
            if (alreadyPariedDevice != null) {
                var indexToRemove = PairedBluetoothDevices.findIndex(x => x.id == device.id);
                PairedBluetoothDevices.splice(indexToRemove, 1);
            }

            PairedBluetoothDevices.push(device);
        });

        return devices.map(x => convertDevice(x));
    }
}

// End Bluetooth


// On disconnected from device

var DeviceDisconnectionHandler = [];

async function onDisconnected() {

    console.log('> Bluetooth Device disconnected.');

    await DeviceDisconnectionHandler.invokeMethodAsync('HandleDeviceDisconnected');
}

window.ble.addDeviceDisconnectionHandler = (deviceDisconnectHandler, deviceUuid) => {
    var device = getPairedBluetoothDeviceById(deviceUuid);

    if (deviceDisconnectHandler !== null) {

        DeviceDisconnectionHandler = deviceDisconnectHandler;
        device.addEventListener('gattserverdisconnected', onDisconnected);
    }
    else if (DeviceDisconnectionHandler !== null && DeviceDisconnectionHandler.length > 0) {

        device.removeEventListener('gattserverdisconnected', onDisconnected);
        DeviceDisconnectionHandler = null;
    }
}

// End On disconnected from device


// On availability changed from bluetooth

var BluetoothAvailabilityHandler = [];

async function onAvailabilityChanged() {

    await BluetoothAvailabilityHandler.invokeMethodAsync('HandleAvailabilityChanged');
}

window.ble.addBluetoothAvailabilityHandler = (bluetoothAvailabilityHandler) => {

    if (bluetoothAvailabilityHandler !== null) {

        BluetoothAvailabilityHandler = bluetoothAvailabilityHandler;
        navigator.bluetooth.addEventListener('onavailabilitychanged', onDisconnected);
    }
    else if (BluetoothAvailabilityHandler !== null && BluetoothAvailabilityHandler.length > 0) {

        navigator.bluetooth.removeEventListener('onavailabilitychanged', onDisconnected);
        BluetoothAvailabilityHandler = null;
    }
}

// End On availability changed from bluetooth