import { Link } from "react-router-dom";
import guitarModel from "../../../Interfaces/guitarModel";

interface Props {
    guitar: guitarModel;
}

function GuitarCard(props: Props) {
  return (
    <div className="col-md-4 col-12 p-4">
      <div
        className="card"
        style={{ boxShadow: "0 1px 7px 0 rgb(0 0 0 / 50%)" }}
      >
        <div className="card-body pt-2">
          <div className="row col-10 offset-1 p-4">
            <Link to={`/guitarDetails/${props.guitar.id}`}>
              <img
                style={{ borderRadius: "50%" }}
                alt=""
                className="w-100 mt-5 image-box"
              />
            </Link>
          </div>
          <div className="text-center">
            <p className="card-title m-0 text-success fs-3">
              <Link
                to={`/guitarDetails/${props.guitar.id}`}
                style={{ textDecoration: "none", color: "green" }}
              >
                {props.guitar.name}
              </Link>
            </p>
            <p className="badge bg-secondary" style={{ fontSize: "12px" }}>
              {props.guitar.category}
            </p>
          </div>
          <p
            className="card-text"
            style={{
              textAlign: "center",
              fontWeight: "light",
              fontSize: "14px",
            }}
          >
            {props.guitar.description}
          </p>
          <div className="row text-center">
            <h4>${props.guitar.price}</h4>
          </div>
        </div>
      </div>
    </div>
  );
}

export default GuitarCard;
