import { configureStore } from "@reduxjs/toolkit";
import { guitarReducer } from "./guitarSlice";
import {
  authApi,
  guitarApi
} from "../../Apis";
import { userAuthReducer } from "./userAuthSlice";
const store = configureStore({
  reducer: {
    guitarReducer: guitarReducer,
    userAuthStore: userAuthReducer,
    [guitarApi.reducerPath]: guitarApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(guitarApi.middleware)
      .concat(authApi.middleware)
});

export type RootState = ReturnType<typeof store.getState>;

export default store;
