import { IIdentifiable } from "./IIdentifiable";

export interface ListEntity extends IIdentifiable<number>
{
    tags: number[],
    html: string
}