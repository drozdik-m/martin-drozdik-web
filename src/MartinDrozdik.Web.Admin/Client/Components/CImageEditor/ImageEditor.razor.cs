﻿using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Abstraction.Services.CRUD;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Admin.Client.Pages;
using MartinDrozdik.Web.Admin.Client.Pages.Projects;
using MartinDrozdik.Web.Admin.Client.Services.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace MartinDrozdik.Web.Admin.Client.Components.CImageEditor
{
    public partial class ImageEditor<TImage> : RootComponent
    {

        /// <summary>
        /// The part with inputs and other tools to edit the entity
        /// </summary>
        [Parameter]
        public RenderFragment<ImageEditorInputsContext<TImage>> ExtraInputs { get; set; }

        /// <summary>
        /// How to display the image
        /// </summary>
        [Parameter, EditorRequired]
        public RenderFragment<TImage> ImageDisplay { get; set; }

        /// <summary>
        /// Tells of the inputs of this components are disabled
        /// </summary>
        [Parameter]
        public bool ReadOnly { get; set; } = false;

        /// <summary>
        /// The image
        /// </summary>
        [Parameter, EditorRequired]
        public TImage Image { get; set; }

        /// <summary>
        /// Cast image into the Image object
        /// </summary>
        protected Image ImageCast
        {
            get
            {
                if (Image is not Image res)
                    throw new Exception("Image could not be cast to Image object");

                return res;
            }
        }

        /*IList<IBrowserFile> files = new List<IBrowserFile>();
        private void UploadFiles(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);
            }
            //TODO upload the files to the server
        }*/


    }
}
