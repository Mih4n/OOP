{ pkgs ? import <nixos> {} }:

pkgs.mkShell {
  buildInputs = with pkgs; [
    dotnet-sdk_9
    dotnet-runtime_9
    dotnet-aspnetcore_9
  ];
}