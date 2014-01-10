using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SnesKit
{
    public enum BankTypeEnum { Lo, Hi };
    public class RomDump
    {
        // Indica si la ROM tiene el smc header o no
        bool SmcHeader;

        // Indica la localización del header de SNES
        int HeaderLocation;

        // Array con los datos de la ROM
        public byte[] Data;

        // Los diferentes datos que obtenemos de la ROM
        public string Name;
        public byte Layout;
        public byte CartridgeType;
        public byte RomSize;
        public byte RamSize;
        public byte CountryCode;
        public byte LicenseCode;
        public byte VersionNumber;
        ushort Checksum;
        ushort ChecksumCompliment;
        public BankTypeEnum BankType;

        // Esta funcion permite el analisis de ROMS de SNES con extensiones SMC y SFC
        public RomDump(byte[] rom)
        {
            this.Data = rom;
            // Comprobamos si existe el header smc
            if (this.Data.Length % 1024 == 512)
                SmcHeader = true;
            else if (this.Data.Length % 1024 == 0)
                SmcHeader = false;
            else
                throw new Exception("Archivo de rom invalida.");
            this.HeaderLocation = 0x81C0;

            if (HeaderIsAt(0x07FC0)) // La Rom es LoROM
            {
                this.BankType = BankTypeEnum.Lo;
            }
            else if (HeaderIsAt(0x0FFC0))
            {
                this.BankType = BankTypeEnum.Hi;
            }

            // Leemos el Header
            ReadHeader();

        }
        // Función para comprobar si el header esta en la dirección correcta
        private bool HeaderIsAt(ushort addr)
        {
            this.HeaderLocation = addr;
            return VerifyChecksum();
        }

        // Offset 0x07FC0 in a headerless LoROM image (LoROM rom sin smc header)
        // Offset 0x0FFC0 in a headerless HiROM image (HiROM rom sin smc header)
        // verifica el checksum
        private bool VerifyChecksum()
        {
            // La rom tiene header smc
            if (SmcHeader)
                this.HeaderLocation += 512;

            this.ChecksumCompliment = BitConverter.ToUInt16(this.Get(0x1C, 0x1D), 0);
            this.Checksum = BitConverter.ToUInt16(this.Get(0x1E, 0x1F), 0);
            ushort ver = (ushort)(this.Checksum ^ this.ChecksumCompliment);
            return (ver == 0xFFFF);
        }

        private void ReadHeader()
        {
            this.Name = Encoding.ASCII.GetString(this.Get(0x00, 0x14)); // 21 chars
            this.Layout = this.At(0x15);
            this.CartridgeType = this.At(0x16);
            this.RomSize = this.At(0x17);
            this.RamSize = this.At(0x18);
            this.CountryCode = this.At(0x19);
            this.LicenseCode = this.At(0x1A);
            this.VersionNumber = this.At(0x1B);
        }

        private string GetROmB()
        {
            return String.Format("{0}", this.RomSize);
        }
        private byte[] Get(int from, int to)
        {
            return this.Data.Skip(this.HeaderLocation + from).Take(to - from + 1).ToArray();
        }
        private byte At(int addr)
        {
            return this.Data[this.HeaderLocation + addr];
        }
    }
}
