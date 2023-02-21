import React, { useState } from "react";
import { Tooltip } from "react-tooltip";
import "react-tooltip/dist/react-tooltip.css";
import { Account } from "../types";
import "./CurrentAccounts.scss";

interface CurrentAccountsProps {
  account: Account;
  currentAccountId: string;
  handleTransfer: Function;
  handleRequest: Function;
  errorMessage: string;
}

export const CurrentAccounts = ({
  account,
  currentAccountId,
  handleTransfer,
  handleRequest,
  errorMessage,
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
          <Tooltip
            anchorId={`input-${account.id}`}
            className="tooltip"
            isOpen={errorMessage === "" ? false : true}
          />
          <input
            type="number"
            value={amount}
            id={`input-${account.id}`}
            data-tooltip-content={errorMessage}
            data-tooltip-place="top"
            data-tooltip-variant="error"
            className="input amount-input"
            placeholder="Value"
            onChange={(e) => setAmount(e.target.value)}
          />

          <div
            className="button button-hover transfer-button"
            onClick={() => handleTransfer(account, amount)}
          >
            <p className="text">Transfer</p>
          </div>
          <div
            className="button button-hover request-button"
            onClick={() => handleRequest(account, amount)}
          >
            <p className="text">Request</p>
          </div>
        </div>
      )}
    </div>
  );
};

export default CurrentAccounts;
