import { ListEntity } from "./ListEntity";


export interface EntityListConfig<TEntity extends ListEntity>
{
    entities: TEntity[],
    loadMoreButton: boolean,
    pageSize: number,
    initialSize: number
}