import { useState, useEffect } from "react";
import GuitarCard from "./GuitarCard";
import { useDispatch } from "react-redux";
import { setGuitar } from "../../../Storage/Redux/guitarSlice";
import { MainLoader } from "../Common";
import guitarModel from "../../../Interfaces/guitarModel";
import { useGetGuitarsQuery } from "../../../Apis/guitarApi";
function GuitarList() {
  const [guitars, setGuitars] = useState<guitarModel[]>([]);
  const dispatch = useDispatch();
  const { data, isLoading } = useGetGuitarsQuery(null);

  useEffect(() => {
    if (!isLoading) {
      dispatch(setGuitar(data.result));
      setGuitars(data.result);
    }
  }, [isLoading]);

  if (isLoading) {
    return <MainLoader />;
  }

  return (
    <div className="container row">
      {guitars.length > 0 &&
        guitars.map((guitar: guitarModel, index: number) => (
          <GuitarCard guitar={guitar} key={index} />
        ))}
    </div>
  );
}

export default GuitarList;
