﻿// -----------------------------------------------------------------------
// <copyright file="FrameElement.cs" company="SimpleBrowser">
// Copyright © 2010 - 2018, Nathan Ridley and the SimpleBrowser contributors.
// See https://github.com/SimpleBrowserDotNet/SimpleBrowser/blob/master/readme.md
// </copyright>
// -----------------------------------------------------------------------

namespace SimpleBrowser.Elements
{
    using System;
    using System.Xml.Linq;

    internal class FrameElement : HtmlElement
    {
        public FrameElement(XElement element)
            : base(element)
        { }

        public Browser FrameBrowser { get; private set; }

        internal override string GetAttributeValue(string name)
        {
            if (name == "SimpleBrowser.WebDriver:frameWindowHandle")
            {
                return this.FrameBrowser.WindowHandle;
            }
            return base.GetAttributeValue(name);
        }

        public string Src
        {
            get => this.Element.GetAttributeCI("src");
        }

        public string Name
        {
            get => this.Element.GetAttributeCI("name");
        }

        internal override Browser OwningBrowser
        {
            get
            {
                return base.OwningBrowser;
            }
            set
            {
                base.OwningBrowser = value;
                this.FrameBrowser = this.OwningBrowser.CreateChildBrowser(this.Name);
                this.FrameBrowser.Navigate(new Uri(this.OwningBrowser.Url, this.Src));
            }
        }
    }
}