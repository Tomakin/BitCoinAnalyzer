import React, { useState } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import { authenticationService } from '../services';
import { useNavigate } from 'react-router-dom';

function LoginForm() {
  let navigate = useNavigate();
  const [hasError, setHasError] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  const formik = useFormik({
    initialValues: {
      username: '',
      password: '',
    },
    validationSchema: Yup.object({
      username: Yup.string()
        .min(3, 'Kullanıcı adı en az 3 karakter olmalıdır.')
        .required('Kullanıcı adı zorunludur.'),
      password: Yup.string()
        .min(6, 'Şifre en az 6 karakter olmalıdır.')
        .required('Şifre zorunludur.'),
    }),
    onSubmit: async (values, { setSubmitting }) => {
      try {
        await authenticationService.login(values.username, values.password);
        setHasError(false);
        navigate("/");        
      } catch (error) {
        setErrorMessage(error);
        setHasError(true);
        console.error('Giriş hatası:', error);
      } finally {
        setSubmitting(false);
      }
    },
  });

  return (
    <form onSubmit={formik.handleSubmit}>
      <div>
      <h2>Giriş Yap</h2>
        <label htmlFor="username">Kullanıcı Adı</label>
        <input
          type="text"
          id="username"
          name="username"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          value={formik.values.username}
        />
        {formik.touched.username && formik.errors.username ? (
          <div className="error">{formik.errors.username}</div>
        ) : null}
      </div>
      <div>
        <label htmlFor="password">Şifre</label>
        <input
          type="password"
          id="password"
          name="password"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          value={formik.values.password}
        />
        {formik.touched.password && formik.errors.password ? (
          <div className="error">{formik.errors.password}</div>
        ) : null}
        {hasError && 
        (
          <div className='error'>
            {errorMessage}
          </div>
        )
        }
      </div>
      <button type="submit" disabled={formik.isSubmitting}>
        Giriş Yap
      </button>
    </form>
  );
}

export {LoginForm};