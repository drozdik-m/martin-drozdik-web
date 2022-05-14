import { IIdentifiable } from "./IIdentifiable";


export interface EntityListConfig<TKey, TEntity extends IIdentifiable<TKey>>
{
    entities: [TEntity],
    loadMoreButton: boolean,
    pageSize: number,
}