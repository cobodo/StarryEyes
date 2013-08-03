﻿using System.Collections.Generic;

namespace StarryEyes.Anomaly.TwitterApi.DataModels
{
    public class TwitterEntity
    {
        public static IEnumerable<TwitterEntity> GetEntities(dynamic json)
        {

            if (json.IsDefined("hashtags"))
            {
                var tags = json.hashtags;
                foreach (var tag in tags)
                {
                    yield return new TwitterEntity(
                        EntityType.Hashtags,
                        (string)tag.text, (string)tag.text,
                        (int)tag.indices[0], (int)tag.indices[1]);
                }
            }
            if (json.IsDefined("media"))
            {
                var medias = json.media;
                foreach (var media in medias)
                {
                    yield return new TwitterEntity(
                        EntityType.Media,
                        (string)media.display_url, (string)media.url, (string)media.media_url,
                        (int)media.indices[0], (int)media.indices[1]);
                }
            }
            if (json.IsDefined("urls"))
            {
                var urls = json.urls;
                foreach (var url in urls)
                {
                    yield return new TwitterEntity(
                        EntityType.Urls,
                        (string)url.display_url, (string)url.expanded_url,
                        (int)url.indices[0], (int)url.indices[1]);
                }
            }
            if (json.IsDefined("user_mentions"))
            {
                var mentions = json.user_mentions;
                foreach (var mention in mentions)
                {
                    yield return new TwitterEntity(
                        EntityType.UserMentions,
                        (string)mention.screen_name, (string)mention.id_str,
                        (int)mention.indices[0], (int)mention.indices[1]);
                }
            }
        }

        public TwitterEntity() { }

        public TwitterEntity(EntityType entityType,
            string display, string original, string media, int start, int end)
            : this(entityType, display, original, start, end)
        {
            this.MediaUrl = media;
        }

        public TwitterEntity(EntityType entityType,
            string display, string original, int start, int end)
        {
            this.EntityType = entityType;
            this.DisplayText = display;
            this.OriginalText = original;
            if (string.IsNullOrEmpty(original))
            {
                this.OriginalText = display;
            }
            this.StartIndex = start;
            this.EndIndex = end;
        }

        /// <summary>
        /// Type of this entity.
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// String which represents displaying text. <para />
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// String that represents original information. <para />
        /// If this entity describes URL, this property may have original(unshortened) url. <para />
        /// If this entity describes User, this property may have numerical ID of user. <para />
        /// Otherwise, it has simply copy of Display string.
        /// </summary>
        public string OriginalText { get; set; }

        /// <summary>
        /// Url of media. used only for Media entity.
        /// </summary>
        public string MediaUrl { get; set; }

        /// <summary>
        /// Start index of this element
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// End index of this element
        /// </summary>
        public int EndIndex { get; set; }
    }

    public enum EntityType
    {
        Media,
        Urls,
        UserMentions,
        Hashtags
    }
}
