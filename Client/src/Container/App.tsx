import { Footer, Header } from "../Components/Layout";
import {
  Home,
  Login,
  NotFound,
  Register
} from "../Pages";
import { Routes, Route } from "react-router-dom";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import jwt_decode from "jwt-decode";
import { userModel } from "../Interfaces";
import { setLoggedInUser } from "../Storage/Redux/userAuthSlice";
import GuitarDetails from "../Pages/GuitarDetails";
import GuitarList from "../Pages/Guitar/GuitarList";
import GuitarUpsert from "../Pages/Guitar/GuitarUpsert";

function App() {
  const dispatch = useDispatch();

  useEffect(() => {
    const localToken = localStorage.getItem("token");
    if (localToken) {
      const { fullName, id, email, role }: userModel = jwt_decode(localToken);
      dispatch(setLoggedInUser({ fullName, id, email, role }));
      }
  }, [dispatch]);

  return (
    <div>
      <Header />
      <div className="pb-5">
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route
            path="/guitarDetails/:guitarId"
            element={<GuitarDetails />}
          ></Route>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/guitar/guitarList" element={<GuitarList />} />
          <Route
            path="/guitar/guitarupsert/:id"
            element={<GuitarUpsert />}
          />
          <Route path="/guitar/guitarupsert" element={<GuitarUpsert />} />
          <Route path="*" element={<NotFound />}></Route>
        </Routes>
      </div>
      <Footer />
    </div>
  );
}

export default App;
