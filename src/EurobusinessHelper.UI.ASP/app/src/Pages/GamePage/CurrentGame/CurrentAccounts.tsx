import React, { useState } from "react";
import "react-tooltip/dist/react-tooltip.css";
import { Account } from "../types";
import "./CurrentAccounts.scss";

interface CurrentAccountsProps {
  account: Account;
  currentAccountId: string;
  handleTransfer: Function;
  handleRequest: Function;
}

export const CurrentAccounts = ({
  account,
  currentAccountId,
  handleTransfer,
  handleRequest,
}: CurrentAccountsProps) => {
  const [amount, setAmount] = useState<string>("");

  return (
    <div className="current-players-list-item">
      <div className="current-player">
        <div className="current-player-title">
          <p className="current-player-name">{account.name}</p>
          <p className="current-player-email">{account.email}</p>
        </div>
        <div className="current-player-balance">{account.balance + "$"}</div>
      </div>
      {account.id !== currentAccountId && (
        <div className="transfer-block">
          <input
            type="number"
            value={amount}
            className="input amount-input"
            placeholder="Value"
            onChange={(e) => setAmount(e.target.value)}
          />

          <div
            className="button button-hover transfer-button"
            onClick={() => {
              handleTransfer(account.id, amount);
              setAmount("");
            }}
          >
            <p className="text">Transfer</p>
          </div>
          <div
            className="button button-hover request-button"
            onClick={() => {
              handleRequest(account.id, amount);
              setAmount("");
            }}
          >
            <p className="text">Request</p>
          </div>
        </div>
      )}
    </div>
  );
};

export default CurrentAccounts;
