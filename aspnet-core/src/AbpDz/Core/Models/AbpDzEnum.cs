using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;
#pragma warning disable 0114

namespace AbpDz.Models
{
    public class AbpDzEnumDto
    {

        public int Id { get; set; }
        public int? ParrentId { get; set; }

        [StringLength(64)]
        public string Code { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public string EntityType { get; set; }
        public string Abbreviation { get; set; }
        public string Display { get; set; }

        public string Value { get; set; }
        public int Order { get; set; }


    }
    public class AbpDzEnum : CreationAuditedAggregateRoot<int>, IEntityDto<int>
    {

        public AbpDzEnum()
        {
            this.Childs = new HashSet<AbpDzEnum>();
        }
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [StringLength(64)]
        public string Code { get; set; }
        public int? ParrentId { get; set; }

        [StringLength(64)]
        public string EntityType { get; set; }

        [StringLength(128)]

        public string Display { get; set; }


        [StringLength(128)]

        public string Group { get; set; }



        [StringLength(128)]

        public string Category { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        public void CopyFrom(AbpDzEnum e)
        {
            if (!string.IsNullOrWhiteSpace(e.EntityType)) EntityType = e.EntityType;
            if (!string.IsNullOrWhiteSpace(e.Category)) Category = e.Category;
            if (!string.IsNullOrWhiteSpace(e.Description)) Description = e.Description;
            if (!string.IsNullOrWhiteSpace(e.Display)) Display = e.Display;
            if (e.ExtraProperties != null) ExtraProperties = e.ExtraProperties;
            if (!string.IsNullOrWhiteSpace(e.Abbreviation)) Abbreviation = e.Abbreviation;
            if (!string.IsNullOrWhiteSpace(e.Value)) Value = e.Value;
            if (!string.IsNullOrWhiteSpace(e.Code)) Code = e.Code;
            if (!string.IsNullOrWhiteSpace(e.Group)) Group = e.Group;
            if (e.ExtraProperties != null) ExtraProperties = e.ExtraProperties;
            if (e.Data != null) Data = e.Data;
            IsSelectable = e.IsSelectable;
            IsStatic = e.IsStatic;
        }
        public AbpDzEnumDto ToDto(string local = null)
        {
            var ret = new AbpDzEnumDto
            {
                Id = Id,
                ParrentId = ParrentId,
                EntityType = EntityType,
                Description = Description,
                Value = Value,
                Display = Display,
                Code = Code,
                Order = Order,
                Group = Group,
                Abbreviation = Abbreviation,
            };
            if (string.IsNullOrEmpty(Code))
            {
                ret.Code = ret.Display;
            }
            if (this.ExtraProperties != null && !string.IsNullOrEmpty(local))
            {
                if (ExtraProperties.ContainsKey("display" + local))
                {
                    ret.Display = ExtraProperties[("display" + local)].ToString();
                }
                if (ExtraProperties.ContainsKey("abbreviation" + local))
                {
                    ret.Display = ExtraProperties[("abbreviation" + local)].ToString();
                }
            }
            return ret;

        }
        [StringLength(64)]
        public string Abbreviation { get; set; }
        [StringLength(256)]
        public string Url { get; set; }

        [StringLength(128)]
        public string Value { get; set; }
        [StringLength(AbpDzConsts.MaxDataLength)]

        public string Data { get; set; }

        public bool IsStatic { get; set; }
        public int Order { get; set; }
        public bool? IsSelectable { get; set; } = true;
        [JsonIgnore]
        public ICollection<AbpDzEnum> Childs { get; set; }
        [JsonIgnore]
        public AbpDzEnum Parrent { get; set; }
        // public static void OnModelCreating(ModelBuilder modelBuilder)
        // {

        //     // modelBuilder.AddJsonFields();
        //     modelBuilder.Entity<AbpDzEnum>(e =>
        //     {
        //         e.HasMany(k => k.Childs).WithOne(k => k.Parrent).HasForeignKey(k => k.ParrentId);
        //         e.HasIndex(k => k.ParrentId);
        //         e.HasIndex(k => k.Value);
        //         e.HasIndex(k => k.Code);
        //         e.Property(m => m.Data)
        //             .HasConversion(new JsonValueConverter<object>());
        //         e.Property(m => m.Schema)
        //             .HasConversion(new JsonValueConverter<object>());

        //     });
        // }
    }
}

