import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  guitar: []
};

export const guitarSlice = createSlice({
  name: "Guitar",
  initialState: initialState,
  reducers: {
    setGuitar: (state, action) => {
       state.guitar = action.payload;
    }
  },
});

export const { setGuitar } = guitarSlice.actions;
export const guitarReducer = guitarSlice.reducer;
