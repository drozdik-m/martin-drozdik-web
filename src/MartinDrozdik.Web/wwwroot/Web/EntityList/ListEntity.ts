import { IIdentifiable } from "./IIdentifiable";

export interface ListEntity extends IIdentifiable<number>
{
    tags: string[],
    html: string
}