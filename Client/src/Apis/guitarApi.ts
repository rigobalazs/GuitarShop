import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const guitarApi = createApi({
  reducerPath: "guitarApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "http://localhost:3000/api/",
    prepareHeaders: (headers: Headers, api) => {
      const token = localStorage.getItem("token");
      token && headers.append("Authorization", "Bearer " + token);
    },
  }),
  tagTypes: ["Guitars"],
  endpoints: (builder) => ({
    getGuitars: builder.query({
      query: () => ({
        url: "guitar",
      }),
      providesTags: ["Guitars"],
    }),
    getGuitarById: builder.query({
      query: (id) => ({
        url: `guitar/${id}`,
      }),
      providesTags: ["Guitars"],
    }),
    createGuitar: builder.mutation({
      query: (data) => ({
        url: "guitar",
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["Guitars"],
    }),
    updateGuitar: builder.mutation({
      query: ({ data, id }) => ({
        url: "guitar/" + id,
        method: "PUT",
        body: data,
      }),
      invalidatesTags: ["Guitars"],
    }),
    deleteGuitar: builder.mutation({
      query: (id) => ({
        url: "guitar/" + id,
        method: "DELETE",
      }),
      invalidatesTags: ["Guitars"],
    }),
  }),
});

export const {
  useGetGuitarsQuery,
  useGetGuitarByIdQuery,
  useCreateGuitarMutation,
  useUpdateGuitarMutation,
  useDeleteGuitarMutation,
} = guitarApi;
export default guitarApi;
