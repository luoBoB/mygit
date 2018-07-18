using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class ResponseResult<T>
    {
        public string notifyType { get; set; }
        public string deviceId { get; set; }
        /// <summary>
        /// 网关Id
        /// </summary>
        public string gatewayId { get; set; }
        public string requestId { get; set; }
        public ServiceData<T> service { get; set; }
    }

    public class ServiceData<T>
    {
        public string serviceId { get; set; }
        public string serviceType { get; set; }
        public T data { get; set; }
        public string eventTime { get; set; }
    }

    public class SmokeData
    {
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int Longitude { get; set; }
        public int Longitude1 { get; set; }
        public int Longitude2 { get; set; }
        public int Longitude3 { get; set; }
        public int Latitude { get; set; }
        public int Latitude1 { get; set; }
        public int Latitude2 { get; set; }
        public int Latitude3 { get; set; }
        public int WaterGage { get; set; }
        public int FuelGas { get; set; }
        public int Smoke { get; set; }
        public int Formaldehyde { get; set; }
        public int AirQuality { get; set; }
        public int SoilHumidity { get; set; }
        public int WaterHeight { get; set; }
        public int Voltage { get; set; }
    }

    public class Alarm
    {
        public int FuelGas { get; set; }
        public int EndFlag { get; set; }
    }
}
