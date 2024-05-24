import {
  useDeleteGuitarMutation,
  useGetGuitarsQuery,
} from "../../Apis/guitarApi";
import { toast } from "react-toastify";
import { MainLoader } from "../../Components/Page/Common";
import { useNavigate } from "react-router";
import guitarModel from "../../Interfaces/guitarModel";
function GuitarList() {
  const [deleteGuitar] = useDeleteGuitarMutation();
  const { data, isLoading } = useGetGuitarsQuery(null);
  const navigate = useNavigate();

  const handleGuitarDelete = async (id: number) => {
    toast.promise(
      deleteGuitar(id),
      {
        pending: "Processing your request...",
        success: "Guitar Deleted Successfully 👌",
        error: "Error encoutnered 🤯",
      },
      {
        theme: "dark",
      }
    );
  };

  return (
    <>
      {isLoading && <MainLoader />}
      {!isLoading && (
        <div className="table p-5">
          <div className="d-flex align-items-center justify-content-between">
            <h1 className="text-success">Guitar List</h1>

            <button
              className="btn btn-success"
              onClick={() => navigate("/guitar/guitarupsert")}
            >
              Add New Guitar
            </button>
          </div>
          <div className="p-2">
            <div className="row border">
              <div className="col-1">ID</div>
              <div className="col-6">Name</div>
              <div className="col-3">Category</div>
              <div className="col-1">Price</div>
              <div className="col-1">Action</div>
            </div>

            {data.result.map((guitar: guitarModel) => {
              return (
                <div className="row border" key={guitar.id}>
                  <div className="col-1">{guitar.id}</div>
                  <div className="col-6">{guitar.name}</div>
                  <div className="col-3">{guitar.category}</div>
                  <div className="col-1">${guitar.price}</div>
                  <div className="col-1">
                    <button className="btn btn-success">
                      <i
                        className="bi bi-pencil-fill"
                        onClick={() =>
                          navigate("/guitar/guitarupsert/" + guitar.id)
                        }
                      ></i>
                    </button>
                    <button
                      className="btn btn-danger mx-2"
                      onClick={() => handleGuitarDelete(guitar.id)}
                    >
                      <i className="bi bi-trash-fill"></i>
                    </button>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      )}
    </>
  );
}

export default GuitarList;
