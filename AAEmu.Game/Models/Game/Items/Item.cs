﻿using System;
using AAEmu.Commons.Network;
using AAEmu.Game.Models.Game.Items.Templates;

namespace AAEmu.Game.Models.Game.Items
{
    public enum ItemFlag {
        None = 0x0,
        SoulBound = 0x1,
        HasUCC = 0x2,
        Secure = 0x4,
        Skinized = 0x8,
        Unpacked = 0x10,
        AuctionWin = 0x20
    }
    
    public class Item : PacketMarshaler
    {
        public byte WorldId { get; set; }
        public ulong Id { get; set; }
        public uint TemplateId { get; set; }
        public ItemTemplate Template { get; set; }
        public SlotType SlotType { get; set; }
        public int Slot { get; set; }
        public byte Grade { get; set; }
        public int Count { get; set; }
        public int LifespanMins { get; set; }
        public uint MadeUnitId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UnsecureTime { get; set; }
        public DateTime UnpackTime { get; set; }
        public byte Flags { get; set; }
        public uint ImageItemTemplateId { get; set; }

        public virtual byte DetailType => 0; // TODO 1.0 max type: 8, at 1.2 max type 9 (size: 9 bytes)

        public Item()
        {
            WorldId = AppConfiguration.Instance.Id;
            Slot = -1;
        }

        public Item(byte worldId)
        {
            WorldId = worldId;
            Slot = -1;
        }

        public Item(ulong id, ItemTemplate template, int count)
        {
            WorldId = AppConfiguration.Instance.Id;
            Id = id;
            TemplateId = template.Id;
            Template = template;
            Count = count;
            Slot = -1;
        }

        public Item(byte worldId, ulong id, ItemTemplate template, int count)
        {
            WorldId = worldId;
            Id = id;
            TemplateId = template.Id;
            Template = template;
            Count = count;
            Slot = -1;
        }

        public override void Read(PacketStream stream)
        {
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(TemplateId);
            // TODO ...
            // if (TemplateId == 0)
            //     return stream;
            stream.Write(Id);
            stream.Write(Grade);
            stream.Write(Flags);
            stream.Write(Count);
            stream.Write(DetailType);
            WriteDetails(stream);
            stream.Write(CreateTime);
            stream.Write(LifespanMins);
            stream.Write(MadeUnitId);
            stream.Write(WorldId);
            stream.Write(UnsecureTime);
            stream.Write(UnpackTime);
            return stream;
        }

        public virtual void ReadDetails(PacketStream stream)
        {
        }

        public virtual void WriteDetails(PacketStream stream)
        {
        }
        
        public virtual bool HasFlag(ItemFlag flag) {
            return (Flags & (byte)flag) == (byte)flag;
        }

        public virtual void SetFlag(ItemFlag flag) {
            Flags |= (byte)flag;
        }

        public virtual void RemoveFlag(ItemFlag flag) {
            Flags &= (byte)~flag;
        }
    }
}
