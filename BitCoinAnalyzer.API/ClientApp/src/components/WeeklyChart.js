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

const WeeklyChart = () => {
  const [weeklyData, setWeeklyData] = useState([]);

  useEffect(() => {
    const fetchWeeklyData = async () => {
      try {
        const response = await httpInstance.get('/BitCoin/weekly');
        setWeeklyData(response.data);
      } catch (error) {
        console.error('Hata:', error);
      }
    };

    fetchWeeklyData();
  }, []);

  const chartData = {
    labels: weeklyData.map((data) => data.timestamp),
    datasets: [
      {
        label: 'Haftalık Bitcoin Fiyatı',
        data: weeklyData.map((data) => data.price),
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
      <h2>Haftalık Bitcoin Fiyat Grafiği</h2>
      <Line data={chartData} options={options} />
    </div>
  );
};

export {WeeklyChart};