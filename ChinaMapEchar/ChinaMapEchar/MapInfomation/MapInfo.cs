using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChinaMapEchar.Enum;

namespace ChinaMapEchar.MapInfomation
{
    public class MapItem
    {
        /// <summary>
        /// 出发城市
        /// </summary>
        public CityEnum.ProvincialCapital From { get; set; }

        /// <summary>
        /// 到达城市
        /// </summary>
        public List<MapToItem> To { get; set; }
    }

    /// <summary>
    /// 地图到达城市数据项
    /// </summary>
    public class MapToItem
    {
        /// <summary>
        /// 到达城市
        /// </summary>
        public CityEnum.ProvincialCapital To { get; set; }

        /// <summary>
        /// 到达城市圆点的直径
        /// </summary>
        public double Diameter { get; set; }

        /// <summary>
        /// 提示 Tip
        /// </summary>
        public string Tip { get; set; }
    }
}
