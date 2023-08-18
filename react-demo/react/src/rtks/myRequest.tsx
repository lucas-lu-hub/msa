import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'




export const pokemonApi = createApi({
  reducerPath: 'pokemonApi',
  baseQuery: fetchBaseQuery({ baseUrl: 'localhost:8000/' }),
  endpoints: (builder) => ({
    // 定义端点
    getPokemonByName: builder.query<string, string>({
      // name 是参数，生成传递给 fetch 的参数
      query: (name) => `pokemon/${name}`,
    }),
  }),
})

// 自动生成的 React Hooks
export const { useGetPokemonByNameQuery } = pokemonApi
