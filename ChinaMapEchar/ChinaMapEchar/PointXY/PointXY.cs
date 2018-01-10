using ChinaMapEchar.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChinaMapEchar.PointXY
{
    class PointXY
    {
        /// <summary>
        /// 迁移源起始坐标集，和达到坐标集
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static Point GetPoint(CityEnum.ProvincialCapital city)
        {
            Point point = new Point(0, 0);
            switch (city)
            {
                case CityEnum.ProvincialCapital.北京:
                    point.X = 625.71145;
                    point.Y = 265.20515;
                    break;
                case CityEnum.ProvincialCapital.天津:
                    point.X = 646.648895;
                    point.Y = 277.719215;
                    break;
                case CityEnum.ProvincialCapital.上海:
                    point.X = 730.844;
                    point.Y = 425.208;
                    break;
                case CityEnum.ProvincialCapital.重庆:
                    point.X = 487.123;
                    point.Y = 469.796;
                    break;
                case CityEnum.ProvincialCapital.石家庄:
                    point.X = 605.527;
                    point.Y = 300.853;
                    break;
                case CityEnum.ProvincialCapital.太原:
                    point.X = 575.685;
                    point.Y = 310.961;
                    break;
                case CityEnum.ProvincialCapital.沈阳:
                    point.X = 725.375;
                    point.Y = 214.217;
                    break;
                case CityEnum.ProvincialCapital.长春:
                    point.X = 742.702;
                    point.Y = 173.786;
                    break;
                case CityEnum.ProvincialCapital.哈尔滨:
                    point.X = 751.847;
                    point.Y = 137.687;
                    break;
                case CityEnum.ProvincialCapital.南京:
                    point.X = 691.682;
                    point.Y = 418.295;
                    break;
                case CityEnum.ProvincialCapital.杭州:
                    point.X = 706.603;
                    point.Y = 446.211;
                    break;
                case CityEnum.ProvincialCapital.合肥:
                    point.X = 661.841;
                    point.Y = 418.295;
                    break;
                case CityEnum.ProvincialCapital.福州:
                    point.X = 706.603;
                    point.Y = 528.516;
                    break;
                case CityEnum.ProvincialCapital.南昌:
                    point.X = 646.439;
                    point.Y = 486.16;
                    break;
                case CityEnum.ProvincialCapital.济南:
                    point.X = 648.845;
                    point.Y = 327.807;
                    break;
                case CityEnum.ProvincialCapital.郑州:
                    point.X = 596.382;
                    point.Y = 371.126;
                    break;
                case CityEnum.ProvincialCapital.武汉:
                    point.X = 617.078;
                    point.Y = 451.506;
                    break;
                case CityEnum.ProvincialCapital.长沙:
                    point.X = 593.975;
                    point.Y = 497.231;
                    break;
                case CityEnum.ProvincialCapital.广州:
                    point.X = 611.303;
                    point.Y = 592.531;
                    break;
                case CityEnum.ProvincialCapital.海口:
                    point.X = 553.545;
                    point.Y = 663.766;
                    break;
                case CityEnum.ProvincialCapital.成都:
                    point.X = 445.73;
                    point.Y = 453.912;
                    break;
                case CityEnum.ProvincialCapital.贵阳:
                    point.X = 492.417;
                    point.Y = 533.811;
                    break;
                case CityEnum.ProvincialCapital.昆明:
                    point.X = 420.22;
                    point.Y = 563.171;
                    break;
                case CityEnum.ProvincialCapital.西安:
                    point.X = 522.259;
                    point.Y = 384.121;
                    break;
                case CityEnum.ProvincialCapital.兰州:
                    point.X = 442.842;
                    point.Y = 354.28;
                    break;
                case CityEnum.ProvincialCapital.西宁:
                    point.X = 408.668;
                    point.Y = 340.321;
                    break;
                case CityEnum.ProvincialCapital.拉萨:
                    point.X = 235.394;
                    point.Y = 452.949;
                    break;
                case CityEnum.ProvincialCapital.南宁:
                    point.X = 520.815;
                    point.Y = 605.046;
                    break;
                case CityEnum.ProvincialCapital.呼和浩特:
                    point.X = 557.877;
                    point.Y = 255.128;
                    break;
                case CityEnum.ProvincialCapital.银川:
                    point.X = 479.422;
                    point.Y = 299.891;
                    break;
                case CityEnum.ProvincialCapital.乌鲁木齐:
                    point.X = 220.474;
                    point.Y = 179.562;
                    break;
                case CityEnum.ProvincialCapital.香港:
                    point.X = 623.817;
                    point.Y = 611.784;
                    break;
                case CityEnum.ProvincialCapital.澳门:
                    point.X = 600.714;
                    point.Y = 615.634;
                    break;
                case CityEnum.ProvincialCapital.台北:
                    point.X = 747.515;
                    point.Y = 545.844;
                    break;
            }
            return point;
        }
    }
}
