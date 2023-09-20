import React, { useState } from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import { httpInstance } from "../services";
import { useNavigate } from "react-router-dom";

const Register = () => {
  const initialValues = {
    username: "",
    password: "",
  };

  const { hasError, setHasError } = useState(false);
  const { errorMessage, setErrorMessage } = useState("");

  let navigate = useNavigate();

  const validationSchema = Yup.object({
    username: Yup.string()
      .required("Kullanıcı adı zorunludur.")
      .min(3, "Kullanıcı adı en az 3 karakter olmalıdır."),
    password: Yup.string()
      .required("Şifre zorunludur.")
      .min(6, "Şifre en az 6 karakter olmalıdır."),
  });

  const onSubmit = async (values, { setFieldError }) => {
    try {
      const response = await httpInstance.get(
        `/account/check-username/${values.username}`
      );
      if (response.status === 200) {
        try {
          const registerResponse = await httpInstance.post(
            "/account/register",
            {
              username: values.username,
              password: values.password,
            }
          );

          if (registerResponse.status === 201) {
            console.log("Kullanıcı başarıyla kaydedildi.");
            navigate("/login");
            setHasError(false);
          }
        } catch (error) {
          setErrorMessage(
            "Kullanıcı kaydetme sırasında bir hata oluştu:",
            error
          );
          setHasError(true);
          console.error(
            "Kullanıcı kaydetme sırasında bir hata oluştu: ",
            error
          );
        }
      } else {
        setErrorMessage("Kullanıcı adı zaten alınmış.");
        setHasError(true);
      }
    } catch (error) {
      console.error(
        "Kullanıcı adı kontrolü sırasında bir hata oluştu: ",
        error
      );
    }
  };

  return (
    <div>
      <h2>Kayıt Ol</h2>
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={onSubmit}
      >
        <Form>
          <div>
            <label htmlFor="username">Kullanıcı Adı:</label>
            <Field type="text" id="username" name="username" />
            <ErrorMessage name="username" component="div" className="error" />
          </div>
          <div>
            <label htmlFor="password">Şifre:</label>
            <Field type="password" id="password" name="password" />
            <ErrorMessage name="password" component="div" className="error" />
          </div>
          {hasError && (
            <div className="error">
              {errorMessage}
            </div>
          )}
          <button type="submit">Kaydol</button>
        </Form>
      </Formik>
    </div>
  );
};

export { Register };
