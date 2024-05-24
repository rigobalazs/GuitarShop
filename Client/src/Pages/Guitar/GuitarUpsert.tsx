import React, { useEffect, useState } from "react";
import { inputHelper, toastNotify } from "../../Helper";
import { useNavigate, useParams } from "react-router-dom";
import { MainLoader } from "../../Components/Page/Common";
import { useCreateGuitarMutation, useGetGuitarByIdQuery, useUpdateGuitarMutation } from "../../Apis/guitarApi";


const guitarData = {
  name: "",
  description: "",
  category: "",
  price: "",
};

function GuitarUpsert() {
  const { id } = useParams();

  const navigate = useNavigate();
  const [guitarInputs, setGuitarInputs] = useState(guitarData);
  const [loading, setLoading] = useState(false);
  const [createGuitar] = useCreateGuitarMutation();
  const [updateGuitar] = useUpdateGuitarMutation();
  const { data } = useGetGuitarByIdQuery(id);

  useEffect(() => {
    if (data && data.result) {
      const tempData = {
        name: data.result.name,
        description: data.result.description,
        specialTag: data.result.specialTag,
        category: data.result.category,
        price: data.result.price,
      };
      setGuitarInputs(tempData);
    }
  }, [data]);

  const handleGuitarInput = (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement
    >
  ) => {
    const tempData = inputHelper(e, guitarInputs);
    setGuitarInputs(tempData);
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);

    const formData = new FormData();

    formData.append("Name", guitarInputs.name);
    formData.append("Description", guitarInputs.description);
    formData.append("Category", guitarInputs.category);
    formData.append("Price", guitarInputs.price);

    let response;

    if (id) {
      //update
      formData.append("Id", id);
      response = await updateGuitar({ data: formData, id });
      toastNotify("Guitar updated successfully", "success");
    } else {
      //create
      response = await createGuitar(formData);
      toastNotify("Guitar created successfully", "success");
    }

    if (response) {
      setLoading(false);
      navigate("/guitar/guitarList");
    }

    setLoading(false);
  };

  return (
    <div className="container border mt-5 p-5 bg-light">
      {loading && <MainLoader />}
      <h3 className=" px-2 text-success">
        {id ? "Edit Guitar" : "Add Guitar"}
      </h3>
      <form method="post" encType="multipart/form-data" onSubmit={handleSubmit}>
        <div className="row mt-3">
          <div className="col-md-7">
            <input
              type="text"
              className="form-control"
              placeholder="Enter Name"
              required
              name="name"
              value={guitarInputs.name}
              onChange={handleGuitarInput}
            />
            <textarea
              className="form-control mt-3"
              placeholder="Enter Description"
              name="description"
              rows={10}
              value={guitarInputs.description}
              onChange={handleGuitarInput}
            ></textarea>
            <input
                type="text"
                className="form-control"
                placeholder="Enter Category"
                required
                name="category"
                value={guitarInputs.category}
                onChange={handleGuitarInput}
            />
            <input
              type="number"
              className="form-control mt-3"
              required
              placeholder="Enter Price"
              name="price"
              value={guitarInputs.price}
              onChange={handleGuitarInput}
            />
            <div className="row">
              <div className="col-6">
                <button
                  type="submit"
                  className="btn btn-success form-control mt-3"
                >
                  {id ? "Update" : "Create"}
                </button>
              </div>
              <div className="col-6">
                <a onClick={() => navigate("/guitar/guitarList")} className="btn btn-secondary form-control mt-3" >
                  Back to Guitars
                </a>
              </div>
            </div>
          </div>
        </div>
      </form>
    </div>
  );
}

export default GuitarUpsert;
