﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The collection of models for public use
/// </summary>
namespace MatioCMS.Includes.Models
{
    #region Configuration
    /// <summary>
    /// Public read-only use models
    /// </summary>

        [Table("Config")]
        public class Config
        {
            [Key, Required, MaxLength(35)]
            public string Name { get; set; }
            public string Value { get; set; }
        }

        [Table("Admins")]
        public class Admin
        {
            [Key, Required, ConcurrencyCheck, MaxLength(35)]
            public string Username { get; set; }
            [Required, MaxLength(128)]
            public string Fullname { get; set; }
            public DateTime DateAdded { get; set; } = DateTime.UtcNow;

            public virtual ICollection<Gallery> AddedGalleryItems { get; set; }
        }

        [Table("Plugins")]
        public class Plugin
        {
            [Key, Required, MaxLength(50), ConcurrencyCheck]
            public string Name { get; set; }
            [DefaultValue(false)]
            public bool Enabled { get; set; }
            [DefaultValue(false)]
            public bool Locked { get; set; }
            public DateTime DateAdded { get; set; } = DateTime.UtcNow;
            public string Settings { get; set; }
        }

        [Table("Themes")]
        public class Theme
        {
            [Key, Required, MaxLength(50)]
            public string Name { get; set; }
            [Required, MaxLength(128)]
            public string Title { get; set; }
            public DateTime DateAdded { get; set; } = DateTime.UtcNow;
            public DateTime DateModified { get; set; } = DateTime.UtcNow;
            public string Settings { get; set; }
        }

        [Table("Errors")]
        public class Error
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            [Required, MaxLength(128)]
            public string ExceptionName { get; set; }
            [Required, DataType(DataType.Url)]
            public string Filename { get; set; }
            [DefaultValue(0)]
            public uint Line { get; set; }
            public string Message { get; set; }
            [Required]
            public string StackTrace { get; set; }
            public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        }
    #endregion

    #region Content
        [Table("Categories")]
        public class Category
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            [Required, MaxLength(35)]
            public string Name { get; set; }
            [Required, MaxLength(128)]
            public string Title { get; set; }
            public DateTime DateAdded { get; set; } = DateTime.UtcNow;
            public DateTime DateModified { get; set; } = DateTime.UtcNow;
            [DefaultValue(null)]
            public int ParentID { get; set; }
            public string Description { get; set; }
            [Range(0, long.MaxValue), DefaultValue(0)]
            public long Views { get; set; }

            public virtual Category Parent { get; set; }
            public virtual ICollection<Category> Children { get; set; }
        }

        [Table("Tags")]
        public class Tag
        {
            [Key, ConcurrencyCheck, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            [Required, MaxLength(35)]
            public string Name { get; set; }
            [Required, MaxLength(128)]
            public string Title { get; set; }
            public DateTime DateAdded { get; set; } = DateTime.UtcNow;
            [Range(0, long.MaxValue), DefaultValue(0)]
            public long Views { get; set; }
        }

        [Table("Widgets")]
        public class Widget
        {
            [Key, ConcurrencyCheck, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            [Required, MaxLength(128)]
            public string WidgetName { get; set; }
            [MaxLength(50)]
            public string AreaName { get; set; }
            [DefaultValue(0)]
            public byte PlaceNumber { get; set; }
            public string Settings { get; set; }
        }

        [Table("Menu")]
        public class Menu
        {
            [Key, Required, MaxLength(50)]
            public string Name { get; set; }
            [MaxLength(50)]
            public string AreaName { get; set; }
            public string Content { get; set; }
        }

        [Table("Links")]
        public class Link
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            [Required, MaxLength(128)]
            public string Title { get; set; }
            [Url, MaxLength(256)]
            public string URL { get; set; }
        }
        
        [Table("Snippets")]
        public class Snippet
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            [Required, StringLength(128, MinimumLength = 3)]
            public string Title { get; set; }
            [Required, StringLength(35,MinimumLength = 2)]
            public string Platform { get; set; }
            [Required, StringLength(35, MinimumLength = 2)]
            public string ObjectType { get; set; }
            [Required, StringLength(128,MinimumLength = 1)]
            public string ObjectID { get; set; }

            public enum SnippetPlatform { Facebook, Twitter, Instagram, GooglePlus, GoogleMaps, YouTube, Pinterest, Flickr, Tumblr }
            public static class ObjectTypes
            {
                public enum Facebook { User, Fanpage, Post }
                public enum Instagram { User, Post }
                public enum Twitter { User, Tweet }
                public enum GooglePlus { User, Post }
                public enum GoogleMaps { Location, Photo, Direction, StreetView }
                public enum YouTube { Video, Playlist, Channel }
                public enum Pinterest { User, Board, Pin }
                public enum Flickr { User, Photo }
                public enum Tumblr { Blog, Post }
            }
        }

        [Table("Gallery")]
        public class Gallery
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            [Required, StringLength(128, MinimumLength = 3)]
            public string Title { get; set; }
            [Required]
            public string Path { get; set; }
            public DateTime DateAdded { get; set; } = DateTime.UtcNow;
            [Required]
            public GalleryCategories Category { get; set; }
            [Required, RegularExpression("^[A-Z]{2,6}$")]
            public string Extension { get; set; }
            public string Description { get; set; }
            [Required, MaxLength(35)]
            public string AuthorUsername { get; set; }

            public virtual Admin Author { get; set; }
        }
        public enum GalleryCategories
        {
            Image = 1,
            Video = 2,
            Audio = 3,
            Document = 4,
            Binary = 5,
            Archive = 6
        }

        public class PublishedPage
        {
            public long ID { get; set; }
            public string Name { get; set; }
            public string Title { get; set; }
            public string Categories { get; set; }
            public string Tags { get; set; }
            public string Content { get; set; }
            public string TextContent { get; set; }
            public string CreatedBy_Username { get; set; }
            public string EditedBy_Username { get; set; }
            public DateTime DateAdded { get; set; }
            public DateTime DateModified { get; set; }
            public DateTime DatePublished { get; set; }
            public ulong Views { get; set; }
        }

        public class PublishedPost
        {
            public ulong ID { get; set; }
            public string Name { get; set; }
            public string Title { get; set; }
            public string CreatedBy_Username { get; set; }
            public string EditedBy_Username { get; set; }
            public string Content { get; set; }
            public string TextContent { get; set; }
            public DateTime DateAdded { get; set; }
            public DateTime DateModified { get; set; }
            public DateTime DatePublished { get; set; }
            public string Categories { get; set; }
            public string Tags { get; set; }
            public bool Pinned { get; set; }
            public ulong Views { get; set; }
        }
    #endregion
}
