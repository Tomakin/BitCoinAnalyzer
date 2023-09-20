import React, { useState } from "react";
import { DailyChart } from "./DailyChart";
import { WeeklyChart } from "./WeeklyChart";
import { MonthlyChart } from "./MonthlyChart";

const GetChart = () => {
  const [selectedChart, setSelectedChart] = useState("daily");

  const handleChartChange = (chartType) => {
    setSelectedChart(chartType);
  };

  return (
    <div>
      <div>
        <button onClick={() => handleChartChange("daily")}>Günlük</button>
        <button onClick={() => handleChartChange("weekly")}>Haftalık</button>
        <button onClick={() => handleChartChange("monthly")}>Aylık</button>
      </div>
      {selectedChart === "daily" && <DailyChart />}
      {selectedChart === "weekly" && <WeeklyChart />}
      {selectedChart === "monthly" && <MonthlyChart />}
    </div>
  );
};

export { GetChart };
