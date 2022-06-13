import { ImageGallery } from "@drozdik.m/image-gallery";

export function MakeGallery(gallerySelector: string)
{
    ImageGallery.FromLinksSelector(`${gallerySelector} li a`);
}