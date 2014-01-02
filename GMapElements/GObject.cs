using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements
{
    public enum GObjectType : byte
    {
        /// <summary>
        /// Светофор
        /// </summary>
        TrafficLight = 1,
        /// <summary>
        /// Станция
        /// </summary>
        Station = 2,
        /// <summary>
        /// Опасное место
        /// </summary>
        DangerousPlace = 3,
        /// <summary>
        /// Мост
        /// </summary>
        Bridge = 4,
        /// <summary>
        /// Переезд
        /// </summary>
        Crossing = 5,
        /// <summary>
        /// Платформа
        /// </summary>
        Platform = 6,
        /// <summary>
        /// Туннель
        /// </summary>
        Tunnel = 7,
        /// <summary>
        /// Стрелка
        /// </summary>
        Switch = 8,
        /// <summary>
        /// Датчик ТКС
        /// </summary>
        Tks = 9,
        /// <summary>
        /// Генератор САУТ
        /// </summary>
        GpuSaut = 10,
        /// <summary>
        /// Тупик
        /// </summary>
        DeadEnd = 11
    }
    public enum AlsnFrequncy : int { Alsn25 = 25, Alsn50 = 50, Alsn75 = 75, NoAlsn = -1, Unknown = 0 }

    [GLength(20)]
    public class GObject : GElement
    {
        public int Ordinate { get; set; }
        public int Length { get; set; }
        public AlsnFrequncy AlsnFreq { get; set; }
        public string Name { get; set; }
        public GObjectType Type { get; private set; }

        protected override void FillWithBytes(byte[] Data)
        {
            Type = (GObjectType)Data[0];
            Length = SubInt(Data, 1, 2);
            Ordinate = SubInt(Data, 7, 3);
            AlsnFreq = AlsnFromCode(Data[6]);
            Name = Encoding.GetEncoding(1251).GetString(Data, 10, 8).Trim();
        }
        protected override byte[] GetBytes()
        {
            var buff = new Byte[DataLength];

            buff[0] = (byte)Type;
            Buffer.BlockCopy(BitConverter.GetBytes(Length), 0, buff, 1, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(Ordinate), 0, buff, 7, 3);
            buff[6] = (byte)CodeFromAlsn(AlsnFreq);
            Buffer.BlockCopy(

            return buff;
        }

        private AlsnFrequncy AlsnFromCode(int alsnCode)
        {
            switch (alsnCode)
            {
                case (0): return AlsnFrequncy.Alsn25;
                case (1): return AlsnFrequncy.Alsn50;
                case (2): return AlsnFrequncy.Alsn75;
                case (3): return AlsnFrequncy.NoAlsn;
                default: return AlsnFrequncy.Unknown;
            }
        }

        private int CodeFromAlsn(AlsnFrequncy Alsn)
        {
            switch (Alsn)
            {
                case AlsnFrequncy.Alsn25: return 0;
                case AlsnFrequncy.Alsn50: return 1;
                case AlsnFrequncy.Alsn75: return 2;
                case AlsnFrequncy.NoAlsn: return 3;
                default: return -1;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", GetObjectTypeShortName(Type), Name != null ? string.Format(" \"{0}\"", Name) : "");
        }

        public static string GetObjectTypeShortName(GObjectType t)
        {
            switch (t)
            {
                case (GObjectType.Bridge): return "МСТ";
                case (GObjectType.Crossing): return "ПРЕ";
                case (GObjectType.DangerousPlace): return "ОПМ";
                case (GObjectType.DeadEnd): return "ТПК";
                case (GObjectType.GpuSaut): return "ГПУ";
                case (GObjectType.Platform): return "ПЛФ";
                case (GObjectType.Station): return "СТЦ";
                case (GObjectType.Switch): return "СТР";
                case (GObjectType.Tks): return "ТКС";
                case (GObjectType.TrafficLight): return "СВФ";
                case (GObjectType.Tunnel): return "ТНЛ";
                default: return t.ToString();
            }
        }
    }
}
