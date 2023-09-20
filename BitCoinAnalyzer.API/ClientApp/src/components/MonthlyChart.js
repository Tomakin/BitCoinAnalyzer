import React, { useEffect, useState } from 'react';
import { httpInstance } from '../services';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
  } from 'chart.js';
import { Line } from 'react-chartjs-2';


ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
  );

const MonthlyChart = () => {
  const [monthlyData, setMonthlyData] = useState([]);

  useEffect(() => {
    const fetchMonthlyData = async () => {
      try {
        const response = await httpInstance.get('/BitCoin/monthly');
        setMonthlyData(response.data);
      } catch (error) {
        console.error('Hata:', error);
      }
    };

    fetchMonthlyData();
  }, []);

  const chartData = {
    labels: monthlyData.map((data) => data.timestamp),
    datasets: [
      {
        label: 'Aylık Bitcoin Fiyatı',
        data: monthlyData.map((data) => data.price),
        fill: false,
        borderColor: 'steelblue',
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top',
      },
      title: {
        display: true,
        text: 'BitCoin Grafiği',
      },
    },
  };

  return (
    <div>
      <h2>Aylık Bitcoin Fiyat Grafiği</h2>
      <Line data={chartData} options={options} />
    </div>
  );
};

export {MonthlyChart};