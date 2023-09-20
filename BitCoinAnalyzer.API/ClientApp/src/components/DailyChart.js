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

const DailyChart = () => {
  const [dailyData, setDailyData] = useState([]);

  useEffect(() => {
    const fetchDailyData = async () => {
      try {
        const response = await httpInstance.get('/BitCoin/daily');
        setDailyData(response.data);
      } catch (error) {
        console.error('Hata:', error);
      }
    };

    fetchDailyData();
  }, []);

  const chartData = {
    labels: dailyData.map((data) => data.timestamp),
    datasets: [
      {
        label: 'Günlük Bitcoin Fiyatı',
        data: dailyData.map((data) => data.price),
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
      <h2>Günlük Bitcoin Fiyat Grafiği</h2>
      <Line data={chartData} options={options} />
    </div>
  );
};

export {DailyChart};